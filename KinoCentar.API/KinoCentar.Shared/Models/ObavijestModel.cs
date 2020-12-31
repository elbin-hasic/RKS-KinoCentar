using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class ObavijestModel
    {
        public int Id { get; set; }

        public int KorisnikId { get; set; }

        public string KorisnikImePrezime
        {
            get
            {
                return Korisnik?.ImePrezime;
            }
        }

        public string Naslov { get; set; }

        public string Tekst { get; set; }

        public DateTime Datum { get; set; }

        public string DatumFormat
        {
            get
            {
                return Datum.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
        }

        public string DatumShortDate
        {
            get
            {
                return Datum.ToShortDateString();
            }
        }

        public KorisnikModel Korisnik { get; set; }
    }
}
