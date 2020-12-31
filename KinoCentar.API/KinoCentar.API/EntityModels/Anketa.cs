using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Anketa
    {
        public Anketa()
        {
            this.Odgovori = new HashSet<AnketaOdgovor>();
        }

        public int Id { get; set; }

        public int KorisnikId { get; set; }

        [MaxLength(250)]
        public string Naslov { get; set; }

        public DateTime Datum { get; set; }

        public DateTime? ZakljucenoDatum { get; set; }

        public virtual Korisnik Korisnik { get; set; }

        public virtual ICollection<AnketaOdgovor> Odgovori { get; set; }
    }
}
