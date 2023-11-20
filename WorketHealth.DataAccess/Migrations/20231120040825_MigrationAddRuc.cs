using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorketHealth.DataAccess.Migrations
{
    public partial class MigrationAddRuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruc",
                columns: table => new
                {
                    ID_RUC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COD_RUC = table.Column<int>(type: "int", nullable: false),
                    NOM_RUC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPCION_RUC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruc", x => x.ID_RUC);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ruc");
        }
    }
}
