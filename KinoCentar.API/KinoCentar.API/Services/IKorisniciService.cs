using KinoCentar.API.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.Services
{
    public interface IKorisniciService
    {
        Korisnik Authentication(string username, string password);
    }
}
