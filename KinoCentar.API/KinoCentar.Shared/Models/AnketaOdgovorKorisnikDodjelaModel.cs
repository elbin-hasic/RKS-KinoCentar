using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class AnketaOdgovorKorisnikDodjelaModel
    {
        public int Id { get; set; }

        public int AnketaOdgovorId { get; set; }

        public int KorisnikId { get; set; }

        public DateTime Datum { get; set; }
    }
}
