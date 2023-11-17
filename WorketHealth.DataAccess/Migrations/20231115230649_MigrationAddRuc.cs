using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorketHealth.DataAccess.Migrations
{
    public partial class MigrationAddRuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DESCRIPCION_RUC",
                table: "Ruc",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NOM_RUC",
                table: "Ruc",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DESCRIPCION_RUC",
                table: "Ruc");

            migrationBuilder.DropColumn(
                name: "NOM_RUC",
                table: "Ruc");
        }
    }
}
