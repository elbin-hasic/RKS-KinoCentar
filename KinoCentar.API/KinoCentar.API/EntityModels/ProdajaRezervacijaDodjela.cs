using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class ProdajaRezervacijaDodjela
    {
        public int Id { get; set; }

        public int ProdajaId { get; set; }

        public int RezervacijaId { get; set; }

        public decimal Cijena { get; set; }

        public virtual Prodaja Prodaja { get; set; }

        public virtual Rezervacija Rezervacija { get; set; }
    }
}
