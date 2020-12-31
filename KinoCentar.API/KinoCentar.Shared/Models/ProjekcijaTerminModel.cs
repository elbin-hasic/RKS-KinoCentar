using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class ProjekcijaTerminModel
    {
        public int Id { get; set; }

        public int ProjekcijaId { get; set; }

        public TimeSpan Termin { get; set; }

        public string TerminShort
        {
            get
            {
                return Termin.ToString(@"hh\:mm");
            }
        }

        public virtual ProjekcijaModel Projekcija { get; set; }
    }
}
