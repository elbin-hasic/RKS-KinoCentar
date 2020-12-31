using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoCentar.API.Migrations
{
    public partial class add_anketa_korisnik_odgovori : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnketaOdgovorKorisnikDodjela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnketaOdgovorId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnketaOdgovorKorisnikDodjela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnketaOdgovorKorisnikDodjela_AnketaOdgovor_AnketaOdgovorId",
                        column: x => x.AnketaOdgovorId,
                        principalTable: "AnketaOdgovor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnketaOdgovorKorisnikDodjela_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnketaOdgovorKorisnikDodjela_AnketaOdgovorId",
                table: "AnketaOdgovorKorisnikDodjela",
                column: "AnketaOdgovorId");

            migrationBuilder.CreateIndex(
                name: "IX_AnketaOdgovorKorisnikDodjela_KorisnikId",
                table: "AnketaOdgovorKorisnikDodjela",
                column: "KorisnikId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnketaOdgovorKorisnikDodjela");
        }
    }
}
