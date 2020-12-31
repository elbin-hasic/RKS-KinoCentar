using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class ProdajaModel
    {
        public int Id { get; set; }

        public string BrojRacuna { get; set; }

        public int? KorisnikId { get; set; }

        public string KorisnikImePrezime
        {
            get
            {
                return Korisnik?.ImePrezime;
            }
        }

        public DateTime Datum { get; set; }

        public decimal? Popust { get; set; }

        public decimal? Porez { get; set; }

        public decimal UkupnaCijena { get; set; }

        public string FilmNaslov { get; set; }

        public string SalaNaziv { get; set; }

        public KorisnikModel Korisnik { get; set; }

        public List<ProdajaRezervacijaDodjelaModel> RezervacijeStavke { get; set; }

        public List<ProdajaArtikalDodjelaModel> ArtikliStavke { get; set; }
    }
}
