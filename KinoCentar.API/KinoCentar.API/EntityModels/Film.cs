using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class Film
    {
        public Film()
        {
            this.Glumci = new HashSet<FilmGlumacDodjela>();
            this.Projekcije = new HashSet<Projekcija>();
        }

        public int Id { get; set; }

        [MaxLength(250)]
        public string Naslov { get; set; }

        public int? Trajanje { get; set; }

        public int? GodinaSnimanja { get; set; }

        [MaxLength(2000)]
        public string Sadrzaj { get; set; }

        [MaxLength(100)]
        public string VideoLink { get; set; }

        [MaxLength(100)]
        public string ImdbLink { get; set; }

        public byte[] Plakat { get; set; }

        public byte[] PlakatThumb { get; set; }

        public int? RediteljId { get; set; }

        public int? ZanrId { get; set; }

        public virtual FilmskaLicnost Reditelj { get; set; }

        public virtual Zanr Zanr { get; set; }

        public virtual ICollection<FilmGlumacDodjela> Glumci { get; set; }

        public virtual ICollection<Projekcija> Projekcije { get; set; }
    }
}
