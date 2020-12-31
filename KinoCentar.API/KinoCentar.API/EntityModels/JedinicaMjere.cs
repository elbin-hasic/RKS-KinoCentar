using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class JedinicaMjere
    {
        public JedinicaMjere()
        {
            this.Artikli = new HashSet<Artikal>();
        }

        public int Id { get; set; }

        public string KratkiNaziv { get; set; }

        public string Naziv { get; set; }

        public virtual ICollection<Artikal> Artikli { get; set; }
    }
}
