using CadastroDeCaminhao.Dominio.Entidades;
using CadastroDeCaminhao.Dominio.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace CadastroDeCaminhao.Data.Mapeamentos
{
    public class CaminhaoMapeamento : IEntityTypeConfiguration<Caminhao>
    {
        public void Configure(EntityTypeBuilder<Caminhao> builder)
        {
            builder.Property(p => p.NomeDoCaminhao)
                .HasColumnName("nome_do_caminho")
                .IsRequired(true)
                .HasMaxLength(300)
                .HasComment("Nome comercial do caminhão para venda");

            builder.Property(p => p.DescricaoSobreOCaminhao)
               .HasColumnName("descricao_sobre_o_caminhao")
               .IsRequired(false)
               .HasMaxLength(300)
               .HasComment("Descrição para os compradores verem");

            builder.Property(p => p.PrecoDoCaminhao)
               .HasColumnName("preco_do_caminhao")
               .IsRequired(true)
               .HasComment("Preco comercial para os compradores verem");

            builder.Property(p => p.Modelo)
               .HasColumnName("modelo")
               .IsRequired(true)
               .HasConversion(new EnumToStringConverter<EModelo>())
               .HasComment("Tipo do modelo do caminhao");

            builder.Property(p => p.AnoDeFabricacao)
               .HasColumnName("ano_de_fabricacao")
               .IsRequired(true)
               .HasComment("O ano pode ser atual, quando o caminhão foi fabricado");

            builder.Property(p => p.AnoModelo)
               .HasColumnName("ano_modelo")
               .IsRequired(true)
               .HasComment("Qual o ano desse modelo fabricado, podendo ser atual ou do ano que vem");

            builder.Property(p => p.ImageBase64)
               .HasColumnName("imagem_base_64")
               .IsRequired(false)
               .HasComment("A imagem storada em banco para os clientes verem o caminhao");

            new EntidadeBaseMapeamento<Caminhao>().AdicionaConfiguracaoComum(builder);
        }
    }
}
