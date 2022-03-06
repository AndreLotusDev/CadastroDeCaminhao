using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CadastroDeCaminhao.Data.Auxiliares
{
    public class EntradaAuditoria
    {
        public EntradaAuditoria(EntityEntry entrada)
        {
            Entrada = entrada;
        }

        public EntityEntry Entrada { get; }
        public string UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string TabelaNome { get; set; }
        public Dictionary<string, object> ChaveValor { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> AntigosValores { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NovosValores { get; } = new Dictionary<string, object>();
        public TipoAuditoria TipoAuditoria { get; set; }
        public List<string> ColunasMudadas { get; } = new List<string>();
        public DateTime HoraCriada { get; set; }
        public Auditoria ParaAuditar()
        {
            return new Auditoria
            {
                UsuarioId = UsuarioId,
                UsuarioNome = UsuarioNome,
                TipoAuditoria = TipoAuditoria.ToString(),
                TabelaNome = TabelaNome,
                HoraCriada = DateTime.Now,
                ChavePrimaria = JsonConvert.SerializeObject(ChaveValor),
                AntigoValores = AntigosValores.Count == 0 ? null : JsonConvert.SerializeObject(AntigosValores),
                NovosValores = NovosValores.Count == 0 ? null : JsonConvert.SerializeObject(NovosValores),
                ColunasAfetadas = ColunasMudadas.Count == 0 ? null : JsonConvert.SerializeObject(ColunasMudadas)
            };
        }
    }

    public class Auditoria
    {
        public Auditoria()
        {

        }

        public long Id { get; set; }
        public string UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string TipoAuditoria { get; set; }
        public string TabelaNome { get; set; }
        public DateTime HoraCriada { get; set; }
        public string AntigoValores { get; set; }
        public string NovosValores { get; set; }
        public string ColunasAfetadas { get; set; }
        public string ChavePrimaria { get; set; }
    }

    public enum TipoAuditoria
    {
        NENHUM = 0,
        CRIADO = 1,
        ATUALIZADO = 2,
        DELETE = 3
    }
}
