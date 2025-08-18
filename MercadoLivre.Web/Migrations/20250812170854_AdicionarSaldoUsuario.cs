using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MercadoLivre.Web.Migrations
{
    public partial class AdicionarSaldoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Usuarios",
                type: "TEXT",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Usuarios");
        }
    }
}
