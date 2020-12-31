using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Korisnik
    {
        public Korisnik()
        {
            this.Ankete = new HashSet<Anketa>();
            this.Dojmovi = new HashSet<Dojam>();
            this.Rezervacije = new HashSet<Rezervacija>();
            this.Obavijesti = new HashSet<Obavijest>();
            this.PosjeteProjekcija = new HashSet<ProjekcijaKorisnikDodjela>();
        }

        public int Id { get; set; }

        public int TipKorisnikaId { get; set; }

        [MaxLength(250)]
        public string KorisnickoIme { get; set; }

        [MaxLength(250)]
        public string LozinkaHash { get; set; }

        [MaxLength(250)]
        public string LozinkaSalt { get; set; }

        [MaxLength(250)]
        public string Ime { get; set; }

        [MaxLength(250)]
        public string Prezime { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(10)]
        public string Spol { get; set; }

        public DateTime? DatumRodjenja { get; set; }

        public byte[] Slika { get; set; }

        public byte[] SlikaThumb { get; set; }

        public virtual TipKorisnika TipKorisnika { get; set; }

        public virtual ICollection<Anketa> Ankete { get; set; }

        public virtual ICollection<Dojam> Dojmovi { get; set; }

        public virtual ICollection<Rezervacija> Rezervacije { get; set; }

        public virtual ICollection<Obavijest> Obavijesti { get; set; }

        public virtual ICollection<ProjekcijaKorisnikDodjela> PosjeteProjekcija { get; set; }
    }
}
