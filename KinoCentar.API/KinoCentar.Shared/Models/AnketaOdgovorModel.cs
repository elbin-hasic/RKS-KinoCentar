using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class AnketaOdgovorModel
    {
        public int Id { get; set; }

        public int AnketaId { get; set; }

        public string Odgovor { get; set; }

        public int RedniBroj { get; set; }

        public int UkupnoIzabrano { get; set; }

        public virtual AnketaModel Anketa { get; set; }
    }
}
