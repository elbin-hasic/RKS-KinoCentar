using AutoMapper;
using KinoCentar.API.EntityModels;
using KinoCentar.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoCentar.API.Services
{
    public class KorisniciService : IKorisniciService
    {
        private readonly KinoCentarDbContext _context;
        private readonly IMapper _mapper;

        public KorisniciService(KinoCentarDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Korisnik Authentication(string username, string password)
        {
            var user = _context.Korisnik.Include(x => x.TipKorisnika).AsNoTracking()
                                 .FirstOrDefault(x => x.KorisnickoIme.ToLower().Equals(username.ToLower()));

            if (user != null)
            {
                var newHash = Shared.Util.UIHelper.GenerateHash(user.LozinkaSalt, password);
                if (newHash == user.LozinkaHash)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
