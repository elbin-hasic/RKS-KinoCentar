using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class FilmskaLicnost
    {
        public FilmskaLicnost()
        {
            this.FilmoviReditelja = new HashSet<Film>();
            this.FilmoviGlumaca = new HashSet<FilmGlumacDodjela>();
        }

        public int Id { get; set; }

        [MaxLength(250)]
        public string Ime { get; set; }

        [MaxLength(250)]
        public string Prezime { get; set; }

        public bool IsReziser { get; set; }

        public bool IsGlumac { get; set; }

        [ForeignKey("RediteljId")]
        public virtual ICollection<Film> FilmoviReditelja { get; set; }

        public virtual ICollection<FilmGlumacDodjela> FilmoviGlumaca { get; set; }
    }
}
