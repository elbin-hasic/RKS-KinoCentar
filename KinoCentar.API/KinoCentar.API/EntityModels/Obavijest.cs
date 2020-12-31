using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Obavijest
    {
        public int Id { get; set; }
        
        public int KorisnikId { get; set; }

        [MaxLength(250)]
        public string Naslov { get; set; }

        [MaxLength(2000)]
        public string Tekst { get; set; }

        public DateTime Datum { get; set; }

        public virtual Korisnik Korisnik { get; set; }
    }
}
