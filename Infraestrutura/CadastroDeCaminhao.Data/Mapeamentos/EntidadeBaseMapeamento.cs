using CadastroDeCaminhao.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CadastroDeCaminhao.Data.Mapeamentos
{
    public class EntidadeBaseMapeamento<T> where T : EntidadeBase
    {
        public void AdicionaConfiguracaoComum(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.CriadoEm)
                .HasColumnName("criado_em")
                .HasComment("Data de criação na tabela referida")
                .IsRequired(false);

            builder.Property(p => p.CriadoPeloUsuarioId)
             .HasColumnName("criado_pelo_usuario_id")
             .HasComment("O usuário que inseriu esse elemnto na tabela refida")
             .IsRequired(false);

            builder.Property(p => p.AtualizadoEm)
             .HasColumnName("atualizado_em")
             .HasComment("Data de atualização desse elemento na tabela referida")
             .IsRequired(false);

            builder.Property(p => p.AtualizadoPeloUsuarioId)
             .HasColumnName("atualizado_pelo_usuario_id")
             .HasComment("Quem atualizou esse elemento na tabela referida")
             .IsRequired(false);
        }
    }
}
