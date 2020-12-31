using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoCentar.API.Migrations
{
    public partial class add_projekcija_termin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_Projekcija_ProjekcijaId",
                table: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_ProjekcijaId",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "ProjekcijaId",
                table: "Rezervacija");

            migrationBuilder.AddColumn<int>(
                name: "ProjekcijaTerminId",
                table: "Rezervacija",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjekcijaTermin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjekcijaId = table.Column<int>(nullable: false),
                    Termin = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjekcijaTermin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjekcijaTermin_Projekcija_ProjekcijaId",
                        column: x => x.ProjekcijaId,
                        principalTable: "Projekcija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_ProjekcijaTerminId",
                table: "Rezervacija",
                column: "ProjekcijaTerminId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjekcijaTermin_ProjekcijaId",
                table: "ProjekcijaTermin",
                column: "ProjekcijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_ProjekcijaTermin_ProjekcijaTerminId",
                table: "Rezervacija",
                column: "ProjekcijaTerminId",
                principalTable: "ProjekcijaTermin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervacija_ProjekcijaTermin_ProjekcijaTerminId",
                table: "Rezervacija");

            migrationBuilder.DropTable(
                name: "ProjekcijaTermin");

            migrationBuilder.DropIndex(
                name: "IX_Rezervacija_ProjekcijaTerminId",
                table: "Rezervacija");

            migrationBuilder.DropColumn(
                name: "ProjekcijaTerminId",
                table: "Rezervacija");

            migrationBuilder.AddColumn<int>(
                name: "ProjekcijaId",
                table: "Rezervacija",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_ProjekcijaId",
                table: "Rezervacija",
                column: "ProjekcijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervacija_Projekcija_ProjekcijaId",
                table: "Rezervacija",
                column: "ProjekcijaId",
                principalTable: "Projekcija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
