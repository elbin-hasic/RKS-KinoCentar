using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Dojam
    {
        public int Id { get; set; }

        public int ProjekcijaId { get; set; }

        public int KorisnikId { get; set; }

        public int Ocjena { get; set; }

        [MaxLength(2000)]
        public string Tekst { get; set; }

        public DateTime Datum { get; set; }

        public virtual Projekcija Projekcija { get; set; }

        public virtual Korisnik Korisnik { get; set; }
    }
}
