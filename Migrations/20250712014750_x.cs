using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Codigo_examen.Migrations
{
    /// <inheritdoc />
    public partial class x : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatosExtras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NumeroExterior = table.Column<int>(type: "int", precision: 1, scale: 999, nullable: true),
                    Colonia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodigoPostal = table.Column<int>(type: "int", precision: 1000, scale: 99999, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatosExtraDelUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosExtras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Datos_extra_usuario",
                        column: x => x.DatosExtraDelUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Contrasena", "IsDeleted", "NombreUsuario" },
                values: new object[,]
                {
                    { new Guid("206c93fb-1500-40a0-9396-c2df231240b7"), "01234567", false, "Juanito" },
                    { new Guid("e6e0f6fc-0000-4e24-adec-8de808a9d75e"), "23456789", false, "Pepito" },
                    { new Guid("e8332afa-665a-4692-a08d-623f06c7cfe3"), "12345678", false, "Daniel" }
                });

            migrationBuilder.InsertData(
                table: "DatosExtras",
                columns: new[] { "Id", "ApellidoMaterno", "ApellidoPaterno", "Calle", "CodigoPostal", "Colonia", "DatosExtraDelUsuario", "Email", "Estado", "Municipio", "NumeroExterior" },
                values: new object[,]
                {
                    { 1, "Example", "Example", "Example", 1000, "Example", new Guid("e8332afa-665a-4692-a08d-623f06c7cfe3"), "Example@example1.com", "Example", "Example", 1 },
                    { 2, "Example", "Example", "Example", 1001, "Example", new Guid("206c93fb-1500-40a0-9396-c2df231240b7"), "Example@example2.com", "Example", "Example", 2 },
                    { 3, "Example", "Example", "Example", 1002, "Example", new Guid("e6e0f6fc-0000-4e24-adec-8de808a9d75e"), "Example@example3.com", "Example", "Example", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatosExtras_DatosExtraDelUsuario",
                table: "DatosExtras",
                column: "DatosExtraDelUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatosExtras");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
