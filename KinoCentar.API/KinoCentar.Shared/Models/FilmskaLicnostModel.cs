using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class FilmskaLicnostModel
    {
        public int Id { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string ImePrezime 
        { 
            get
            {
                return $"{Ime} {Prezime}";
            }
        }

        public bool IsReziser { get; set; }

        public bool IsGlumac { get; set; }
    }
}
