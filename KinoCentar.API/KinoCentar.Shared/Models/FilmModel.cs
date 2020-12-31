using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class FilmModel
    {
        public int Id { get; set; }

        public string Naslov { get; set; }

        public int? Trajanje { get; set; }

        public int? GodinaSnimanja { get; set; }

        public string Sadrzaj { get; set; }

        public string VideoLink { get; set; }

        public string ImdbLink { get; set; }

        public int? RediteljId { get; set; }

        public string RediteljImePrezime 
        {
            get 
            {
                return Reditelj?.ImePrezime;
            }
        }

        public int? ZanrId { get; set; }

        public string ZanrNaziv
        {
            get
            {
                return Zanr?.Naziv;
            }
        }

        public byte[] Plakat { get; set; }

        public byte[] PlakatThumb { get; set; }

        public FilmskaLicnostModel Reditelj { get; set; }

        public ZanrModel Zanr { get; set; }
    }
}
