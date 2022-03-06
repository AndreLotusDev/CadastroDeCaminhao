using CadastroDeCaminhao.Aplicacao.Interfaces;
using CadastroDeCaminhao.Data.Auxiliares;
using CadastroDeCaminhao.Data.Mapeamentos;
using CadastroDeCaminhao.Dominio.Entidades;
using CadastroDeCaminhao.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeCaminhao.Data.Contexto
{
    public class Banco : DbContext
    {
        private readonly IAtualInformacaoUsuario _usuarioAtual;

        public Banco()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EModelo>();
        }
        public Banco(DbContextOptions<Banco> options) : base(options)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EModelo>();
        }

        public Banco(DbContextOptions<Banco> options, IAtualInformacaoUsuario usuarioAtual) : base(options)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EModelo>();

            _usuarioAtual = usuarioAtual;
        }

        public virtual DbSet<Auditoria> Auditorias { get; set; }
        public virtual DbSet<Caminhao> Caminhoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<EModelo>();

            modelBuilder.ApplyConfiguration(new CaminhaoMapeamento());
            
        }

        public async Task<int> SalvaAlteracoesAsync()
        {
            foreach (var entidade in ChangeTracker.Entries<EntidadeBase>().ToList())
            {
                switch (entidade.State)
                {
                    case EntityState.Added:
                        entidade.Entity.AtualizadoEm = DateTime.UtcNow;
                        entidade.Entity.CriadoPeloUsuarioId = _usuarioAtual?.UsuarioId ?? "0";
                        break;

                    case EntityState.Modified:
                        entidade.Entity.AtualizadoEm = DateTime.UtcNow;
                        entidade.Entity.AtualizadoPeloUsuarioId = _usuarioAtual?.UsuarioId?? "0";
                        break;
                }
            }

            if (_usuarioAtual?.UsuarioId != null) await LogDeAuditoria();
            return await base.SaveChangesAsync();
        }

        public async Task LogDeAuditoria()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<EntradaAuditoria>();
            foreach (var entrada in ChangeTracker.Entries())
            {
                if (entrada.Entity is Auditoria || entrada.State == EntityState.Detached || entrada.State == EntityState.Unchanged)
                    continue;
                var entradaAuditoria = new EntradaAuditoria(entrada)
                {
                    TabelaNome = entrada.Entity.GetType().Name,
                    UsuarioId = _usuarioAtual.UsuarioId,
                    UsuarioNome = _usuarioAtual.UsuarioNome
                };
                auditEntries.Add(entradaAuditoria);
                foreach (var propriedade in entrada.Properties)
                {
                    var propertyName = propriedade.Metadata.Name;
                    if (propriedade.Metadata.IsPrimaryKey())
                    {
                        entradaAuditoria.ChaveValor[propertyName] = propriedade.CurrentValue;
                        continue;
                    }
                    switch (entrada.State)
                    {
                        case EntityState.Added:
                            entradaAuditoria.TipoAuditoria = TipoAuditoria.CRIADO;
                            entradaAuditoria.NovosValores[propertyName] = propriedade.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            entradaAuditoria.TipoAuditoria = TipoAuditoria.DELETE;
                            entradaAuditoria.AntigosValores[propertyName] = propriedade.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (propriedade.IsModified)
                            {
                                entradaAuditoria.ColunasMudadas.Add(propertyName);
                                entradaAuditoria.TipoAuditoria = TipoAuditoria.ATUALIZADO;
                                entradaAuditoria.AntigosValores[propertyName] = propriedade.OriginalValue;
                                entradaAuditoria.NovosValores[propertyName] = propriedade.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                await Auditorias.AddAsync(auditEntry.ParaAuditar());
            }
        }
    }
}
