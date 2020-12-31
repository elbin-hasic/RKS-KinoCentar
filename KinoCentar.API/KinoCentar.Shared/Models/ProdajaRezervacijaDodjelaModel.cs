using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class ProdajaRezervacijaDodjelaModel
    {
        public int Id { get; set; }

        public int ProdajaId { get; set; }

        public int RezervacijaId { get; set; }

        public decimal Cijena { get; set; }

        public ProdajaModel Prodaja { get; set; }

        public RezervacijaModel Rezervacija { get; set; }
    }
}
