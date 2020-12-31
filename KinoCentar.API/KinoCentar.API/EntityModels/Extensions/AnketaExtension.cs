using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.EntityModels.Extensions
{
    public class AnketaExtension : Anketa
    {
        public int? KorisnikAnketaOdgovorId { get; set; }

        public virtual AnketaOdgovor KorisnikAnketaOdgovor { get; set; }

        public AnketaExtension()
        { }

        public AnketaExtension(Anketa anketa)
        {
            Id = anketa.Id;
            KorisnikId = anketa.KorisnikId;
            Naslov = anketa.Naslov;
            Datum = anketa.Datum;
            ZakljucenoDatum = anketa.ZakljucenoDatum;

            Korisnik = anketa.Korisnik;
            Odgovori = anketa.Odgovori;
        }
    }
}
