using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KinoCentar.API.EntityModels
{
    public partial class Rezervacija
    {
        public Rezervacija()
        {
            this.Prodaja = new HashSet<ProdajaRezervacijaDodjela>();
        }

        public int Id { get; set; }

        public int ProjekcijaTerminId { get; set; }

        public int? KorisnikId { get; set; }

        public int BrojSjedista { get; set; }

        public decimal Cijena { get; set; }

        public DateTime Datum { get; set; }

        public DateTime DatumProjekcije { get; set; }

        public DateTime? DatumProdano { get; set; }

        public DateTime? DatumOtkazano { get; set; }

        public virtual ProjekcijaTermin ProjekcijaTermin { get; set; }

        public virtual Korisnik Korisnik { get; set; }

        public virtual ICollection<ProdajaRezervacijaDodjela> Prodaja { get; set; }
    }
}
