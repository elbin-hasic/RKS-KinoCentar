using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class DojamModel
    {
        public int Id { get; set; }

        public int ProjekcijaId { get; set; }

        public string ProjekcijaNaslov
        {
            get
            {
                return Projekcija?.Film?.Naslov;
            }
        }

        public int KorisnikId { get; set; }

        public string KorisnikImePrezime
        {
            get
            {
                return Korisnik?.ImePrezime;
            }
        }

        public int Ocjena { get; set; }

        public string Tekst { get; set; }

        public DateTime Datum { get; set; }

        public string DatumFormat
        {
            get
            {
                return Datum.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            }
        }

        public ProjekcijaModel Projekcija { get; set; }

        public KorisnikModel Korisnik { get; set; }
    }
}
