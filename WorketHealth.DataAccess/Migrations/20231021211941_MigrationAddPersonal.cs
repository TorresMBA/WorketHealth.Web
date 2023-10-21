using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorketHealth.DataAccess.Migrations
{
    public partial class MigrationAddPersonal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMenuAccess_Menu_MenuId",
                table: "UserMenuAccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMenuAccess",
                table: "UserMenuAccess");

            migrationBuilder.DropIndex(
                name: "IX_UserMenuAccess_UserId",
                table: "UserMenuAccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserMenuAccess");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMenuAccess",
                table: "UserMenuAccess",
                columns: new[] { "UserId", "MenuId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Anho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Primer_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Segundo_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Primer_Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Segundo_Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fec_Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserMenuAccess_Menus_MenuId",
                table: "UserMenuAccess",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMenuAccess_Menus_MenuId",
                table: "UserMenuAccess");

            migrationBuilder.DropTable(
                name: "Anho");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMenuAccess",
                table: "UserMenuAccess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserMenuAccess",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMenuAccess",
                table: "UserMenuAccess",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMenuAccess_UserId",
                table: "UserMenuAccess",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMenuAccess_Menu_MenuId",
                table: "UserMenuAccess",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
