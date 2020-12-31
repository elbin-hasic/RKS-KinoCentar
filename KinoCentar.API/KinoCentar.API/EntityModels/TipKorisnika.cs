using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class TipKorisnika
    {
        public TipKorisnika()
        {
            this.Korisnici = new HashSet<Korisnik>();
        }

        public int Id { get; set; }

        [MaxLength(250)]
        public string Naziv { get; set; }

        public virtual ICollection<Korisnik> Korisnici { get; set; }
    }
}
