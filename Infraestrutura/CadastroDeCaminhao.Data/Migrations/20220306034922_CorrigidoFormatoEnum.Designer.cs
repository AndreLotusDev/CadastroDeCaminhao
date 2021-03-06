// <auto-generated />
using System;
using CadastroDeCaminhao.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CadastroDeCaminhao.Data.Migrations
{
    [DbContext(typeof(Banco))]
    [Migration("20220306034922_CorrigidoFormatoEnum")]
    partial class CorrigidoFormatoEnum
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasPostgresEnum(null, "e_modelo", new[] { "fm", "fh" })
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("CadastroDeCaminhao.Data.Auxiliares.Auditoria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AntigoValores")
                        .HasColumnType("text");

                    b.Property<string>("ChavePrimaria")
                        .HasColumnType("text");

                    b.Property<string>("ColunasAfetadas")
                        .HasColumnType("text");

                    b.Property<DateTime>("HoraCriada")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NovosValores")
                        .HasColumnType("text");

                    b.Property<string>("TabelaNome")
                        .HasColumnType("text");

                    b.Property<string>("TipoAuditoria")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioNome")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Auditorias");
                });

            modelBuilder.Entity("CadastroDeCaminhao.Dominio.Entidades.Caminhao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("AnoDeFabricacao")
                        .HasColumnType("integer")
                        .HasColumnName("ano_de_fabricacao")
                        .HasComment("O ano pode ser atual, quando o caminhão foi fabricado");

                    b.Property<int>("AnoModelo")
                        .HasColumnType("integer")
                        .HasColumnName("ano_modelo")
                        .HasComment("Qual o ano desse modelo fabricado, podendo ser atual ou do ano que vem");

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("atualizado_em")
                        .HasComment("Data de atualização desse elemento na tabela referida");

                    b.Property<string>("AtualizadoPeloUsuarioId")
                        .HasColumnType("text")
                        .HasColumnName("atualizado_pelo_usuario_id")
                        .HasComment("Quem atualizou esse elemento na tabela referida");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("criado_em")
                        .HasComment("Data de criação na tabela referida");

                    b.Property<string>("CriadoPeloUsuarioId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("criado_pelo_usuario_id")
                        .HasComment("O usuário que inseriu esse elemnto na tabela refida");

                    b.Property<string>("DescricaoSobreOCaminhao")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("descricao_sobre_o_caminhao")
                        .HasComment("Descrição para os compradores verem");

                    b.Property<string>("ImageBase64")
                        .HasColumnType("text")
                        .HasColumnName("imagem_base_64")
                        .HasComment("A imagem storada em banco para os clientes verem o caminhao");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modelo")
                        .HasComment("Tipo do modelo do caminhao");

                    b.Property<string>("NomeDoCaminhao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("nome_do_caminho")
                        .HasComment("Nome comercial do caminhão para venda");

                    b.Property<decimal?>("PrecoDoCaminhao")
                        .IsRequired()
                        .HasColumnType("numeric")
                        .HasColumnName("preco_do_caminhao")
                        .HasComment("Preco comercial para os compradores verem");

                    b.HasKey("Id");

                    b.ToTable("Caminhoes");
                });
#pragma warning restore 612, 618
        }
    }
}
