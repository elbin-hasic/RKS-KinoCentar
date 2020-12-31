using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.EntityModels.Extensions
{
    public class RezervacijaExtension : Rezervacija
    {
        public int? ProjekcijaId { get; set; }
    }
}
