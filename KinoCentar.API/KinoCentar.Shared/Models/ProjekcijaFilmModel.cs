using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class ProjekcijaFilmModel
    {
        public class Row
        {
            public int ProjekcijaId { get; set; }

            public int FilmId { get; set; }

            public string Naslov { get; set; }

            public string Sadrzaj { get; set; }

            public decimal Cijena { get; set; }

            public string Zanr { get; set; }

            public byte[] Plakat { get; set; }

            public byte[] PlakatThumb { get; set; }

            public DateTime Datum { get; set; }

            public DateTime VrijediOd { get; set; }

            public DateTime VrijediDo { get; set; }

            public TimeSpan? Termin { get; set; }

            public List<ProjekcijaFilmTerminModel> Termini { get; set; }
        }

        public List<Row> Rows;
    }
}
