using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorketHealth.DataAccess.Migrations
{
    public partial class MigrationAddTablasF_SEG_19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aptitudes",
                columns: table => new
                {
                    ID_APTITUD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aptitudes", x => x.ID_APTITUD);
                });

            migrationBuilder.CreateTable(
                name: "EnfermedadesComunes",
                columns: table => new
                {
                    ID_ENFERMEDAD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfermedadesComunes", x => x.ID_ENFERMEDAD);
                });

            migrationBuilder.CreateTable(
                name: "EnfermedadesProfesionales",
                columns: table => new
                {
                    ID_ENFERMEDAD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfermedadesProfesionales", x => x.ID_ENFERMEDAD);
                });

            migrationBuilder.CreateTable(
                name: "EnfermedadesRelacionadasTrabajo",
                columns: table => new
                {
                    ID_ENFERMEDAD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnfermedadesRelacionadasTrabajo", x => x.ID_ENFERMEDAD);
                });

            migrationBuilder.CreateTable(
                name: "TipoExamenes",
                columns: table => new
                {
                    ID_TIPO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoExamenes", x => x.ID_TIPO);
                });

            migrationBuilder.CreateTable(
                name: "SeguimientoMedicos",
                columns: table => new
                {
                    ID_SEG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_TIPO_EXAMEN = table.Column<int>(type: "int", nullable: false),
                    FECHA_EXAM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AREA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PUESTO_DE_TRABAJO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_SEG_APT = table.Column<int>(type: "int", nullable: false),
                    RESTRICIONES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RUC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ANHO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguimientoMedicos", x => x.ID_SEG);
                    table.ForeignKey(
                        name: "FK_SeguimientoMedicos_Aptitudes_ID_SEG_APT",
                        column: x => x.ID_SEG_APT,
                        principalTable: "Aptitudes",
                        principalColumn: "ID_APTITUD",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguimientoMedicos_TipoExamenes_ID_TIPO_EXAMEN",
                        column: x => x.ID_TIPO_EXAMEN,
                        principalTable: "TipoExamenes",
                        principalColumn: "ID_TIPO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguimientoEnfermedad",
                columns: table => new
                {
                    SeguimientoMedicoId = table.Column<int>(type: "int", nullable: false),
                    EnfermedadComunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguimientoEnfermedad", x => new { x.SeguimientoMedicoId, x.EnfermedadComunId });
                    table.ForeignKey(
                        name: "FK_SeguimientoEnfermedad_EnfermedadesComunes_EnfermedadComunId",
                        column: x => x.EnfermedadComunId,
                        principalTable: "EnfermedadesComunes",
                        principalColumn: "ID_ENFERMEDAD",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguimientoEnfermedad_SeguimientoMedicos_SeguimientoMedicoId",
                        column: x => x.SeguimientoMedicoId,
                        principalTable: "SeguimientoMedicos",
                        principalColumn: "ID_SEG",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguimientoEnfermedadProfesional",
                columns: table => new
                {
                    SeguimientoMedicoId = table.Column<int>(type: "int", nullable: false),
                    EnfermedadProfesionalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguimientoEnfermedadProfesional", x => new { x.SeguimientoMedicoId, x.EnfermedadProfesionalId });
                    table.ForeignKey(
                        name: "FK_SeguimientoEnfermedadProfesional_EnfermedadesProfesionales_EnfermedadProfesionalId",
                        column: x => x.EnfermedadProfesionalId,
                        principalTable: "EnfermedadesProfesionales",
                        principalColumn: "ID_ENFERMEDAD",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguimientoEnfermedadProfesional_SeguimientoMedicos_SeguimientoMedicoId",
                        column: x => x.SeguimientoMedicoId,
                        principalTable: "SeguimientoMedicos",
                        principalColumn: "ID_SEG",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguimientoEnfermedadTrabajo",
                columns: table => new
                {
                    SeguimientoMedicoId = table.Column<int>(type: "int", nullable: false),
                    EnfermedadRelacionadaTrabajoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguimientoEnfermedadTrabajo", x => new { x.SeguimientoMedicoId, x.EnfermedadRelacionadaTrabajoId });
                    table.ForeignKey(
                        name: "FK_SeguimientoEnfermedadTrabajo_EnfermedadesRelacionadasTrabajo_EnfermedadRelacionadaTrabajoId",
                        column: x => x.EnfermedadRelacionadaTrabajoId,
                        principalTable: "EnfermedadesRelacionadasTrabajo",
                        principalColumn: "ID_ENFERMEDAD",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguimientoEnfermedadTrabajo_SeguimientoMedicos_SeguimientoMedicoId",
                        column: x => x.SeguimientoMedicoId,
                        principalTable: "SeguimientoMedicos",
                        principalColumn: "ID_SEG",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoEnfermedad_EnfermedadComunId",
                table: "SeguimientoEnfermedad",
                column: "EnfermedadComunId");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoEnfermedadProfesional_EnfermedadProfesionalId",
                table: "SeguimientoEnfermedadProfesional",
                column: "EnfermedadProfesionalId");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoEnfermedadTrabajo_EnfermedadRelacionadaTrabajoId",
                table: "SeguimientoEnfermedadTrabajo",
                column: "EnfermedadRelacionadaTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoMedicos_ID_SEG_APT",
                table: "SeguimientoMedicos",
                column: "ID_SEG_APT");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoMedicos_ID_TIPO_EXAMEN",
                table: "SeguimientoMedicos",
                column: "ID_TIPO_EXAMEN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeguimientoEnfermedad");

            migrationBuilder.DropTable(
                name: "SeguimientoEnfermedadProfesional");

            migrationBuilder.DropTable(
                name: "SeguimientoEnfermedadTrabajo");

            migrationBuilder.DropTable(
                name: "EnfermedadesComunes");

            migrationBuilder.DropTable(
                name: "EnfermedadesProfesionales");

            migrationBuilder.DropTable(
                name: "EnfermedadesRelacionadasTrabajo");

            migrationBuilder.DropTable(
                name: "SeguimientoMedicos");

            migrationBuilder.DropTable(
                name: "Aptitudes");

            migrationBuilder.DropTable(
                name: "TipoExamenes");
        }
    }
}
