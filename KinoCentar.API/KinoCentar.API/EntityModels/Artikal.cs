using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Artikal
    {
        public Artikal()
        {
            this.Prodaja = new HashSet<ProdajaArtikalDodjela>();
        }

        public int Id { get; set; }

        public int JedinicaMjereId { get; set; }

        [MaxLength(20)]
        public string Sifra { get; set; }

        [MaxLength(250)]
        public string Naziv { get; set; }

        public decimal Cijena { get; set; }

        public byte[] Slika { get; set; }

        public byte[] SlikaThumb { get; set; }

        public virtual JedinicaMjere JedinicaMjere { get; set; }

        public virtual ICollection<ProdajaArtikalDodjela> Prodaja { get; set; }
    }
}
