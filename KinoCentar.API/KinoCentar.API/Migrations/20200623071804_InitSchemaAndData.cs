using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoCentar.API.Migrations
{
    public partial class InitSchemaAndData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmskaLicnost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(maxLength: 250, nullable: true),
                    Prezime = table.Column<string>(maxLength: 250, nullable: true),
                    IsReziser = table.Column<bool>(nullable: false),
                    IsGlumac = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmskaLicnost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JedinicaMjere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KratkiNaziv = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JedinicaMjere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 250, nullable: true),
                    BrojSjedista = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipKorisnika",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipKorisnika", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 250, nullable: true),
                    Opis = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artikal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JedinicaMjereId = table.Column<int>(nullable: false),
                    Sifra = table.Column<string>(maxLength: 20, nullable: true),
                    Naziv = table.Column<string>(maxLength: 250, nullable: true),
                    Cijena = table.Column<decimal>(nullable: false),
                    Slika = table.Column<byte[]>(nullable: true),
                    SlikaThumb = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artikal_JedinicaMjere_JedinicaMjereId",
                        column: x => x.JedinicaMjereId,
                        principalTable: "JedinicaMjere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipKorisnikaId = table.Column<int>(nullable: false),
                    KorisnickoIme = table.Column<string>(maxLength: 250, nullable: true),
                    LozinkaHash = table.Column<string>(maxLength: 250, nullable: true),
                    LozinkaSalt = table.Column<string>(maxLength: 250, nullable: true),
                    Ime = table.Column<string>(maxLength: 250, nullable: true),
                    Prezime = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: true),
                    Spol = table.Column<string>(maxLength: 10, nullable: true),
                    DatumRodjenja = table.Column<DateTime>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true),
                    SlikaThumb = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Korisnik_TipKorisnika_TipKorisnikaId",
                        column: x => x.TipKorisnikaId,
                        principalTable: "TipKorisnika",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(maxLength: 250, nullable: true),
                    Trajanje = table.Column<int>(nullable: true),
                    GodinaSnimanja = table.Column<int>(nullable: true),
                    Sadrzaj = table.Column<string>(maxLength: 2000, nullable: true),
                    VideoLink = table.Column<string>(maxLength: 100, nullable: true),
                    ImdbLink = table.Column<string>(maxLength: 100, nullable: true),
                    Plakat = table.Column<byte[]>(nullable: true),
                    PlakatThumb = table.Column<byte[]>(nullable: true),
                    RediteljId = table.Column<int>(nullable: true),
                    ZanrId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Film_FilmskaLicnost_RediteljId",
                        column: x => x.RediteljId,
                        principalTable: "FilmskaLicnost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Film_Zanr_ZanrId",
                        column: x => x.ZanrId,
                        principalTable: "Zanr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Anketa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    Naslov = table.Column<string>(maxLength: 250, nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    ZakljucenoDatum = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anketa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anketa_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    Naslov = table.Column<string>(maxLength: 250, nullable: true),
                    Tekst = table.Column<string>(maxLength: 2000, nullable: true),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obavijest_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prodaja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojRacuna = table.Column<string>(nullable: true),
                    KorisnikId = table.Column<int>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Popust = table.Column<decimal>(nullable: true),
                    Porez = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodaja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodaja_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilmGlumacDodjela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(nullable: false),
                    FilmskaLicnostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGlumacDodjela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmGlumacDodjela_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGlumacDodjela_FilmskaLicnost_FilmskaLicnostId",
                        column: x => x.FilmskaLicnostId,
                        principalTable: "FilmskaLicnost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekcija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(nullable: false),
                    SalaId = table.Column<int>(nullable: false),
                    Cijena = table.Column<decimal>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    VrijediOd = table.Column<DateTime>(nullable: false),
                    VrijediDo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekcija_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projekcija_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnketaOdgovor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnketaId = table.Column<int>(nullable: false),
                    Odgovor = table.Column<string>(maxLength: 1000, nullable: true),
                    RedniBroj = table.Column<int>(nullable: false),
                    UkupnoIzabrano = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnketaOdgovor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnketaOdgovor_Anketa_AnketaId",
                        column: x => x.AnketaId,
                        principalTable: "Anketa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdajaArtikalDodjela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdajaId = table.Column<int>(nullable: false),
                    ArtikalId = table.Column<int>(nullable: false),
                    Cijena = table.Column<decimal>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdajaArtikalDodjela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdajaArtikalDodjela_Artikal_ArtikalId",
                        column: x => x.ArtikalId,
                        principalTable: "Artikal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdajaArtikalDodjela_Prodaja_ProdajaId",
                        column: x => x.ProdajaId,
                        principalTable: "Prodaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dojam",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjekcijaId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false),
                    Tekst = table.Column<string>(maxLength: 2000, nullable: true),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dojam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dojam_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dojam_Projekcija_ProjekcijaId",
                        column: x => x.ProjekcijaId,
                        principalTable: "Projekcija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjekcijaKorisnikDodjela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjekcijaId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    DatumPosjete = table.Column<DateTime>(nullable: false),
                    DatumPosljednjePosjete = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjekcijaKorisnikDodjela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjekcijaKorisnikDodjela_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjekcijaKorisnikDodjela_Projekcija_ProjekcijaId",
                        column: x => x.ProjekcijaId,
                        principalTable: "Projekcija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjekcijaId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: true),
                    BrojSjedista = table.Column<int>(nullable: false),
                    Cijena = table.Column<decimal>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    DatumProjekcije = table.Column<DateTime>(nullable: false),
                    DatumProdano = table.Column<DateTime>(nullable: true),
                    DatumOtkazano = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Projekcija_ProjekcijaId",
                        column: x => x.ProjekcijaId,
                        principalTable: "Projekcija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdajaRezervacijaDodjela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdajaId = table.Column<int>(nullable: false),
                    RezervacijaId = table.Column<int>(nullable: false),
                    Cijena = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdajaRezervacijaDodjela", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdajaRezervacijaDodjela_Prodaja_ProdajaId",
                        column: x => x.ProdajaId,
                        principalTable: "Prodaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdajaRezervacijaDodjela_Rezervacija_RezervacijaId",
                        column: x => x.RezervacijaId,
                        principalTable: "Rezervacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anketa_KorisnikId",
                table: "Anketa",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_AnketaOdgovor_AnketaId",
                table: "AnketaOdgovor",
                column: "AnketaId");

            migrationBuilder.CreateIndex(
                name: "IX_Artikal_JedinicaMjereId",
                table: "Artikal",
                column: "JedinicaMjereId");

            migrationBuilder.CreateIndex(
                name: "IX_Dojam_KorisnikId",
                table: "Dojam",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Dojam_ProjekcijaId",
                table: "Dojam",
                column: "ProjekcijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Film_RediteljId",
                table: "Film",
                column: "RediteljId");

            migrationBuilder.CreateIndex(
                name: "IX_Film_ZanrId",
                table: "Film",
                column: "ZanrId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGlumacDodjela_FilmId",
                table: "FilmGlumacDodjela",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGlumacDodjela_FilmskaLicnostId",
                table: "FilmGlumacDodjela",
                column: "FilmskaLicnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Korisnik_TipKorisnikaId",
                table: "Korisnik",
                column: "TipKorisnikaId");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijest_KorisnikId",
                table: "Obavijest",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodaja_KorisnikId",
                table: "Prodaja",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdajaArtikalDodjela_ArtikalId",
                table: "ProdajaArtikalDodjela",
                column: "ArtikalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdajaArtikalDodjela_ProdajaId",
                table: "ProdajaArtikalDodjela",
                column: "ProdajaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdajaRezervacijaDodjela_ProdajaId",
                table: "ProdajaRezervacijaDodjela",
                column: "ProdajaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdajaRezervacijaDodjela_RezervacijaId",
                table: "ProdajaRezervacijaDodjela",
                column: "RezervacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcija_FilmId",
                table: "Projekcija",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcija_SalaId",
                table: "Projekcija",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjekcijaKorisnikDodjela_KorisnikId",
                table: "ProjekcijaKorisnikDodjela",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjekcijaKorisnikDodjela_ProjekcijaId",
                table: "ProjekcijaKorisnikDodjela",
                column: "ProjekcijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KorisnikId",
                table: "Rezervacija",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_ProjekcijaId",
                table: "Rezervacija",
                column: "ProjekcijaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnketaOdgovor");

            migrationBuilder.DropTable(
                name: "Dojam");

            migrationBuilder.DropTable(
                name: "FilmGlumacDodjela");

            migrationBuilder.DropTable(
                name: "Obavijest");

            migrationBuilder.DropTable(
                name: "ProdajaArtikalDodjela");

            migrationBuilder.DropTable(
                name: "ProdajaRezervacijaDodjela");

            migrationBuilder.DropTable(
                name: "ProjekcijaKorisnikDodjela");

            migrationBuilder.DropTable(
                name: "Anketa");

            migrationBuilder.DropTable(
                name: "Artikal");

            migrationBuilder.DropTable(
                name: "Prodaja");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "JedinicaMjere");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Projekcija");

            migrationBuilder.DropTable(
                name: "TipKorisnika");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "FilmskaLicnost");

            migrationBuilder.DropTable(
                name: "Zanr");
        }
    }
}
