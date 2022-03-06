using CadastroDeCaminhao.Dominio.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroDeCaminhao.Data.Migrations
{
    public partial class CorrigidoFormatoEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "modelo",
                table: "Caminhoes",
                type: "text",
                nullable: false,
                comment: "Tipo do modelo do caminhao",
                oldClrType: typeof(EModelo),
                oldType: "e_modelo",
                oldComment: "Tipo do modelo do caminhao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<EModelo>(
                name: "modelo",
                table: "Caminhoes",
                type: "e_modelo",
                nullable: false,
                comment: "Tipo do modelo do caminhao",
                oldClrType: typeof(string),
                oldType: "text",
                oldComment: "Tipo do modelo do caminhao");
        }
    }
}
