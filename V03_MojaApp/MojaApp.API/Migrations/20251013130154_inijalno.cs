using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MojaApp.API.Migrations
{
    /// <inheritdoc />
    public partial class inijalno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opstina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opstina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojIndeksa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpstinaRodjenjaId = table.Column<int>(type: "int", nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlikaStudenta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_Opstina_OpstinaRodjenjaId",
                        column: x => x.OpstinaRodjenjaId,
                        principalTable: "Opstina",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_OpstinaRodjenjaId",
                table: "Student",
                column: "OpstinaRodjenjaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Opstina");
        }
    }
}
