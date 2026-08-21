using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VolunManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRolYRelacionVoluntario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "Voluntarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Persona que participa en jornadas y tareas de voluntariado.", "Voluntario" },
                    { 2, "Organiza jornadas y supervisa el trabajo de los voluntarios.", "Coordinador" },
                    { 3, "Administra el sistema y gestiona los datos generales.", "Administrador" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voluntarios_RolId",
                table: "Voluntarios",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Voluntarios_Roles_RolId",
                table: "Voluntarios",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voluntarios_Roles_RolId",
                table: "Voluntarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Voluntarios_RolId",
                table: "Voluntarios");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "Voluntarios");
        }
    }
}
