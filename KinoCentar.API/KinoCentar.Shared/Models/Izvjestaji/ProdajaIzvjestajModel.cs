using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models.Izvjestaji
{
    public class ProdajaIzvjestajModel
    {
        public string BrojRacuna { get; set; }

        public string Korisnik { get; set; }

        public string Datum { get; set; }

        public decimal Cijena { get; set; }
    }
}
