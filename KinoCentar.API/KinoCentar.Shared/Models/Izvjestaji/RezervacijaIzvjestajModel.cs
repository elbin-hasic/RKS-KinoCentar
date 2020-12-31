using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KinoCentar.Shared.Models.Izvjestaji
{
    public class RezervacijaIzvjestajModel
    {
        public string FilmNaslov { get; set; }

        public string SalaNaziv { get; set; }

        public int BrojSjedista { get; set; }

        public decimal Cijena { get; set; }

        public string TerminProjekcije { get; set; }

        public string KorisnikImePrezime { get; set; }
    }
}
