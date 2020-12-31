using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class KorisnikModel
    {
        public int Id { get; set; }

        public int TipKorisnikaId { get; set; }

        public string TipKorisnikaNaziv 
        {
            get 
            {
                return TipKorisnika?.Naziv;
            }
        }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        public string LozinkaHash { get; set; }

        public string LozinkaSalt { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string ImePrezime
        {
            get
            {
                return $"{Ime} {Prezime}";
            }
        }

        public string Email { get; set; }

        public string Spol { get; set; }

        public DateTime? DatumRodjenja { get; set; }

        public string DatumRodjenjaShortDate
        {
            get
            {
                return DatumRodjenja?.ToShortDateString();
            }
        }

        public byte[] Slika { get; set; }

        public byte[] SlikaThumb { get; set; }

        public TipKorisnikaModel TipKorisnika { get; set; }
    }
}
