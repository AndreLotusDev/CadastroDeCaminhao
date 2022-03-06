using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroDeCaminhao.Data.Migrations
{
    public partial class AdicionadoOpcionalInformacaoDeLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "criado_pelo_usuario_id",
                table: "Caminhoes",
                type: "text",
                nullable: true,
                comment: "O usuário que inseriu esse elemnto na tabela refida",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "O usuário que inseriu esse elemnto na tabela refida");

            migrationBuilder.AlterColumn<DateTime>(
                name: "criado_em",
                table: "Caminhoes",
                type: "timestamp without time zone",
                nullable: true,
                comment: "Data de criação na tabela referida",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldComment: "Data de criação na tabela referida");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "criado_pelo_usuario_id",
                table: "Caminhoes",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "O usuário que inseriu esse elemnto na tabela refida",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComment: "O usuário que inseriu esse elemnto na tabela refida");

            migrationBuilder.AlterColumn<DateTime>(
                name: "criado_em",
                table: "Caminhoes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Data de criação na tabela referida",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true,
                oldComment: "Data de criação na tabela referida");
        }
    }
}
