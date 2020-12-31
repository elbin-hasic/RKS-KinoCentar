using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Prodaja
    {
        public Prodaja()
        {
            this.RezervacijeStavke = new HashSet<ProdajaRezervacijaDodjela>();
            this.ArtikliStavke = new HashSet<ProdajaArtikalDodjela>();
        }

        public int Id { get; set; }

        public string BrojRacuna { get; set; }

        public int? KorisnikId { get; set; }

        public DateTime Datum { get; set; }

        public decimal? Popust { get; set; }

        public decimal? Porez { get; set; }

        public virtual Korisnik Korisnik { get; set; }

        public virtual ICollection<ProdajaRezervacijaDodjela> RezervacijeStavke { get; set; }

        public virtual ICollection<ProdajaArtikalDodjela> ArtikliStavke { get; set; }
    }
}
