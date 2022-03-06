using System;
using CadastroDeCaminhao.Dominio.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CadastroDeCaminhao.Data.Migrations
{
    public partial class AdicionadoCaminhaoTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:e_modelo", "fm,fh");

            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<string>(type: "text", nullable: true),
                    UsuarioNome = table.Column<string>(type: "text", nullable: true),
                    TipoAuditoria = table.Column<string>(type: "text", nullable: true),
                    TabelaNome = table.Column<string>(type: "text", nullable: true),
                    HoraCriada = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AntigoValores = table.Column<string>(type: "text", nullable: true),
                    NovosValores = table.Column<string>(type: "text", nullable: true),
                    ColunasAfetadas = table.Column<string>(type: "text", nullable: true),
                    ChavePrimaria = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caminhoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_do_caminho = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false, comment: "Nome comercial do caminhão para venda"),
                    descricao_sobre_o_caminhao = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true, comment: "Descrição para os compradores verem"),
                    preco_do_caminhao = table.Column<decimal>(type: "numeric", nullable: false, comment: "Preco comercial para os compradores verem"),
                    modelo = table.Column<EModelo>(type: "e_modelo", nullable: false, comment: "Tipo do modelo do caminhao"),
                    ano_de_fabricacao = table.Column<int>(type: "integer", nullable: false, comment: "O ano pode ser atual, quando o caminhão foi fabricado"),
                    ano_modelo = table.Column<int>(type: "integer", nullable: false, comment: "Qual o ano desse modelo fabricado, podendo ser atual ou do ano que vem"),
                    imagem_base_64 = table.Column<string>(type: "text", nullable: true, comment: "A imagem storada em banco para os clientes verem o caminhao"),
                    criado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Data de criação na tabela referida"),
                    criado_pelo_usuario_id = table.Column<string>(type: "text", nullable: false, comment: "O usuário que inseriu esse elemnto na tabela refida"),
                    atualizado_em = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, comment: "Data de atualização desse elemento na tabela referida"),
                    atualizado_pelo_usuario_id = table.Column<string>(type: "text", nullable: true, comment: "Quem atualizou esse elemento na tabela referida")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhoes", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "Caminhoes");
        }
    }
}
