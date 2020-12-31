using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.EntityModels
{
    public class ProjekcijaKorisnikDodjela
    {
        public int Id { get; set; }

        public int ProjekcijaId { get; set; }

        public int KorisnikId { get; set; }

        public DateTime DatumPosjete { get; set; }

        public DateTime DatumPosljednjePosjete { get; set; }

        public virtual Projekcija Projekcija { get; set; }

        public virtual Korisnik Korisnik { get; set; }
    }
}
