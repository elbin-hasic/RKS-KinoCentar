using KinoCentar.Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KinoCentar.Shared.Models
{
    public class TipKorisnikaModel
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public TipKorisnikaType? Type 
        {
            get 
            {
                switch (Naziv)
                {
                    case "Administrator":
                        return TipKorisnikaType.Administrator;
                    case "Moderator":
                        return TipKorisnikaType.Moderator;
                    case "Radnik":
                        return TipKorisnikaType.Radnik;
                    case "Klijent":
                        return TipKorisnikaType.Klijent;
                }

                return null;
            }
        }
    }
}
