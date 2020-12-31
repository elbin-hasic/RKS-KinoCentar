using KinoCentar.API.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class KinoCentarDbContext : DbContext
    {
        public KinoCentarDbContext(DbContextOptions<KinoCentarDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProdajaRezervacijaDodjela>()
            .HasOne(c => c.Rezervacija)
            .WithMany(c => c.Prodaja)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AnketaOdgovorKorisnikDodjela>()
            .HasOne(c => c.AnketaOdgovor)
            .WithMany(c => c.OdgovoriKorisnika)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Anketa> Anketa { get; set; }
        public DbSet<AnketaOdgovor> AnketaOdgovor { get; set; }
        public DbSet<AnketaOdgovorKorisnikDodjela> AnketaOdgovorKorisnikDodjela { get; set; }
        public DbSet<Artikal> Artikal { get; set; }
        public DbSet<Dojam> Dojam { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmGlumacDodjela> FilmGlumacDodjela { get; set; }
        public DbSet<FilmskaLicnost> FilmskaLicnost { get; set; }
        public DbSet<JedinicaMjere> JedinicaMjere { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Obavijest> Obavijest { get; set; }
        public DbSet<Prodaja> Prodaja { get; set; }
        public DbSet<ProdajaArtikalDodjela> ProdajaArtikalDodjela { get; set; }
        public DbSet<ProdajaRezervacijaDodjela> ProdajaRezervacijaDodjela { get; set; }
        public DbSet<Projekcija> Projekcija { get; set; }
        public DbSet<ProjekcijaKorisnikDodjela> ProjekcijaKorisnikDodjela { get; set; }
        public DbSet<ProjekcijaTermin> ProjekcijaTermin { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<TipKorisnika> TipKorisnika { get; set; }
        public DbSet<Zanr> Zanr { get; set; }
    }
}
