using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class FilmGlumacDodjela
    {
        public int Id { get; set; }

        public int FilmId { get; set; }

        public int FilmskaLicnostId { get; set; }

        public virtual Film Film { get; set; }

        public virtual FilmskaLicnost FilmskaLicnost { get; set; }
    }
}
