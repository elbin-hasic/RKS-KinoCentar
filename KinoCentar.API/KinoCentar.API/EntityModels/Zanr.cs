using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Zanr
    {
        public Zanr()
        {
            this.Filmovi = new HashSet<Film>();
        }

        public int Id { get; set; }

        [MaxLength(250)]
        public string Naziv { get; set; }

        [MaxLength(2000)]
        public string Opis { get; set; }

        public virtual ICollection<Film> Filmovi { get; set; }
    }
}
