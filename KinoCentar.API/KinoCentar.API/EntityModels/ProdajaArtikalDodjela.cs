using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class ProdajaArtikalDodjela
    {
        public int Id { get; set; }

        public int ProdajaId { get; set; }

        public int ArtikalId { get; set; }

        public decimal Cijena { get; set; }

        public int Kolicina { get; set; }

        public virtual Prodaja Prodaja { get; set; }

        public virtual Artikal Artikal { get; set; }
    }
}
