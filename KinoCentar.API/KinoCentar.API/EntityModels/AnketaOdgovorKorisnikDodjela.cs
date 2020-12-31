using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.EntityModels
{
    public class AnketaOdgovorKorisnikDodjela
    {
        public int Id { get; set; }

        public int AnketaOdgovorId { get; set; }

        public int KorisnikId { get; set; }

        public DateTime Datum { get; set; }

        public virtual AnketaOdgovor AnketaOdgovor { get; set; }

        public virtual Korisnik Korisnik { get; set; }
    }
}
