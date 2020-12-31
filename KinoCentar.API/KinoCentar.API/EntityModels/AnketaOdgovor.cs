using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public class AnketaOdgovor
    {
        public AnketaOdgovor()
        {
            this.OdgovoriKorisnika = new HashSet<AnketaOdgovorKorisnikDodjela>();
        }

        public int Id { get; set; }

        public int AnketaId { get; set; }

        [MaxLength(1000)]
        public string Odgovor { get; set; }

        public int RedniBroj { get; set; }

        public int UkupnoIzabrano { get; set; }

        public virtual Anketa Anketa { get; set; }

        public virtual ICollection<AnketaOdgovorKorisnikDodjela> OdgovoriKorisnika { get; set; }
    }
}
