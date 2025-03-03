using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modelo.Migrations
{
    /// <inheritdoc />
    public partial class Migracion24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumnosAuditoria_Personas_PersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.AlterColumn<int>(
                name: "PersonaId",
                table: "AlumnosAuditoria",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AlumnosAuditoria_Personas_PersonaId",
                table: "AlumnosAuditoria",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlumnosAuditoria_Personas_PersonaId",
                table: "AlumnosAuditoria");

            migrationBuilder.AlterColumn<int>(
                name: "PersonaId",
                table: "AlumnosAuditoria",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlumnosAuditoria_Personas_PersonaId",
                table: "AlumnosAuditoria",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
