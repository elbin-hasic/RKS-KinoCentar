using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.EntityModels
{
    public class ProjekcijaTermin
    {
        public ProjekcijaTermin()
        {
            this.Rezervacije = new HashSet<Rezervacija>();
        }

        public int Id { get; set; }

        public int ProjekcijaId { get; set; }

        public TimeSpan Termin { get; set; }

        public virtual Projekcija Projekcija { get; set; }

        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
    }
}
