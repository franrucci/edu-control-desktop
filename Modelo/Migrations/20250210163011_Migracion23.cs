using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modelo.Migrations
{
    /// <inheritdoc />
    public partial class Migracion23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumnosAuditoria_Personas_AlumnoPersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.DropIndex(
                name: "IX_AlumnosAuditoria_AlumnoPersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.DropColumn(
                name: "AlumnoPersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.CreateIndex(
                name: "IX_AlumnosAuditoria_PersonaId",
                table: "AlumnosAuditoria",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumnosAuditoria_Personas_PersonaId",
                table: "AlumnosAuditoria",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumnosAuditoria_Personas_PersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.DropIndex(
                name: "IX_AlumnosAuditoria_PersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.AddColumn<int>(
                name: "AlumnoPersonaId",
                table: "AlumnosAuditoria",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlumnosAuditoria_AlumnoPersonaId",
                table: "AlumnosAuditoria",
                column: "AlumnoPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumnosAuditoria_Personas_AlumnoPersonaId",
                table: "AlumnosAuditoria",
                column: "AlumnoPersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId");
        }
    }
}
