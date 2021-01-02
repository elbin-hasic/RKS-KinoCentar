using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KinoCentar.API.EntityModels;
using KinoCentar.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using KinoCentar.Shared.Models.Enums;
using System.Net;
using KinoCentar.Shared;
using KinoCentar.Shared.Extensions;
using KinoCentar.Shared.Util;
using KinoCentar.API.EntityModels.Extensions;

namespace KinoCentar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public KorisniciController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Korisnici
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Korisnik>>> GetKorisnik()
        {
            return await _context.Korisnik.Include(i => i.TipKorisnika).AsNoTracking().ToListAsync();
        }

        // GET: api/Korisnici/Klijenti
        [HttpGet]
        [Route("Klijenti")]
        public async Task<ActionResult<IEnumerable<Korisnik>>> GetKorisnikKlijenti()
        {
            return await _context.Korisnik
                                 .Include(i => i.TipKorisnika).AsNoTracking()
                                 .Where(x => x.TipKorisnika.Naziv.ToLower() == TipKorisnikaType.Klijent.ToString().ToLower())
                                 .ToListAsync();
        }

        // GET: api/Korisnici/SearchByName/{firstName}/{lastName}/{onlyClients}
        [HttpGet]
        [Route("SearchByName/{firstName}/{lastName}/{onlyClients}")]
        public async Task<ActionResult<IEnumerable<Korisnik>>> GetKorisnik(string firstName, string lastName, bool onlyClients)
        {
            List<Korisnik> korisnici = new List<Korisnik>();

            if (!string.IsNullOrEmpty(firstName) && firstName != "*" && !string.IsNullOrEmpty(lastName) && lastName != "*")
            {
                korisnici = await _context.Korisnik.Where(x => x.Ime.Contains(firstName) || x.Prezime.Contains(lastName)).Include(i => i.TipKorisnika).AsNoTracking().ToListAsync();
            }
            else if (!string.IsNullOrEmpty(firstName) && firstName != "*")
            {
                korisnici = await _context.Korisnik.Where(x => x.Ime.Contains(firstName)).Include(i => i.TipKorisnika).AsNoTracking().ToListAsync();
            }
            else if (!string.IsNullOrEmpty(lastName) && lastName != "*")
            {
                korisnici = await _context.Korisnik.Where(x => x.Prezime.Contains(lastName)).Include(i => i.TipKorisnika).AsNoTracking().ToListAsync();
            }
            else
            {
                korisnici = await _context.Korisnik.Include(i => i.TipKorisnika).AsNoTracking().ToListAsync();
            }

            if (onlyClients)
            {
                korisnici = korisnici.Where(x => x.TipKorisnika.Naziv.ToLower() == TipKorisnikaType.Klijent.ToString().ToLower()).ToList();
            }

            return korisnici;
        }

        // GET: api/Korisnici/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Korisnik>> GetKorisnik(int id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return korisnik;
        }

        // GET: api/Korisnici/GetByUserName/{userName}/{isClient}
        [HttpGet]
        [Route("GetByUserName/{userName}/{isClient}")]
        public async Task<ActionResult<Korisnik>> GetKorisnik(string userName, bool isClient)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            Korisnik korisnik = null;
            if (isClient)
            {
                korisnik = await _context.Korisnik
                                 .Include(x => x.TipKorisnika).AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.TipKorisnika.Naziv.ToLower() == TipKorisnikaType.Klijent.ToString().ToLower() && 
                                                           x.KorisnickoIme.ToLower().Equals(userName.ToLower()));
            }
            else
            {
                korisnik = await _context.Korisnik
                                 .Include(x => x.TipKorisnika).AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.TipKorisnika.Naziv.ToLower() != TipKorisnikaType.Klijent.ToString().ToLower() && 
                                                           x.KorisnickoIme.ToLower().Equals(userName.ToLower()));
            }

            if (korisnik == null)
            {
                return NotFound();
            }

            return korisnik;
        }

        // POST: api/Korisnici/CheckLogin
        [HttpPost]
        [Route("CheckLogin")]
        public async Task<ActionResult<Korisnik>> CheckLogin(LoginModel model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest();
            }

            Korisnik korisnik = await _context.Korisnik
                                 .Include(x => x.TipKorisnika).AsNoTracking()
                                 .FirstOrDefaultAsync(x => x.TipKorisnika.Naziv.ToLower() == TipKorisnikaType.Klijent.ToString().ToLower() &&
                                                           x.KorisnickoIme.ToLower().Equals(model.UserName.ToLower()));

            if (korisnik == null)
            {
                return NotFound();
            }

            var lozinkaHash = UIHelper.GenerateHash(korisnik.LozinkaSalt, model.Password);
            if (korisnik.LozinkaHash.Equals(lozinkaHash, StringComparison.InvariantCultureIgnoreCase))
            {
                return korisnik;
            }
            else
            {
                return NotFound();
            }            
        }

        // PUT: api/Korisnici/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKorisnik(int id, KorisnikExtension korisnik)
        {
            if (id != korisnik.Id)
            {
                return BadRequest();
            }

            if (KorisnikExists(korisnik.KorisnickoIme, korisnik.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.korisnik_err);
            }

            if (KorisnikExistsByEmail(korisnik.Email, korisnik.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.korisnik_email_err);
            }

            var editUser = await _context.Korisnik.FindAsync(id);
            if (editUser == null)
            {
                return NotFound();
            }

            try
            {
                editUser.Ime = korisnik.Ime;
                editUser.Prezime = korisnik.Prezime;
                editUser.Email = korisnik.Email;
                editUser.DatumRodjenja = korisnik.DatumRodjenja;
                editUser.Spol = korisnik.Spol;

                editUser.KorisnickoIme = korisnik.KorisnickoIme;
                editUser.Slika = korisnik.Slika;
                editUser.SlikaThumb = korisnik.SlikaThumb;

                if (string.IsNullOrEmpty(korisnik.LozinkaHash) || string.IsNullOrEmpty(korisnik.LozinkaHash))
                {
                    if (!string.IsNullOrEmpty(korisnik.Lozinka))
                    {
                        korisnik.LozinkaSalt = UIHelper.GenerateSalt();
                        korisnik.LozinkaHash = UIHelper.GenerateHash(korisnik.LozinkaSalt, korisnik.Lozinka);
                    }
                }
                else
                {
                    editUser.LozinkaSalt = korisnik.LozinkaSalt;
                    editUser.LozinkaHash = korisnik.LozinkaHash;
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KorisnikExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Korisnici/Registracija
        [HttpPost]
        [Route("Registracija")]
        public async Task<ActionResult<Korisnik>> PostRegistracijaKorisnik(KorisnikExtension korisnik)
        {
            if (string.IsNullOrEmpty(korisnik.KorisnickoIme))
            {
                return BadRequest();
            }

            if (KorisnikExists(korisnik.KorisnickoIme))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.korisnik_err);
            }

            if (KorisnikExistsByEmail(korisnik.Email))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.korisnik_email_err);
            }

            if (string.IsNullOrEmpty(korisnik.LozinkaHash) || string.IsNullOrEmpty(korisnik.LozinkaHash))
            {
                if (string.IsNullOrEmpty(korisnik.Lozinka))
                {
                    return BadRequest();
                }

                korisnik.LozinkaSalt = UIHelper.GenerateSalt();
                korisnik.LozinkaHash = UIHelper.GenerateHash(korisnik.LozinkaSalt, korisnik.Lozinka);
            }

            var tipKorisnik = await _context.TipKorisnika.FirstOrDefaultAsync(x => x.Naziv.ToLower() == TipKorisnikaType.Klijent.ToString().ToLower());
            if (tipKorisnik == null)
            {
                return Conflict();
            }

            korisnik.TipKorisnikaId = tipKorisnik.Id;

            _context.Korisnik.Add(korisnik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKorisnik", new { id = korisnik.Id }, korisnik);
        }

        // POST: api/Korisnici
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Korisnik>> PostKorisnik(Korisnik korisnik)
        {
            if (string.IsNullOrEmpty(korisnik.KorisnickoIme))
            {
                return BadRequest();
            }

            if (KorisnikExists(korisnik.KorisnickoIme))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.korisnik_err);
            }

            if (KorisnikExistsByEmail(korisnik.Email))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.korisnik_email_err);
            }

            _context.Korisnik.Add(korisnik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKorisnik", new { id = korisnik.Id }, korisnik);
        }

        // DELETE: api/Korisnici/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Korisnik>> DeleteKorisnik(int id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            if (korisnik == null)
            {
                return NotFound();
            }

            try
            {
                _context.Korisnik.Remove(korisnik);
                await _context.SaveChangesAsync();

                return korisnik;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool KorisnikExists(int id)
        {
            return _context.Korisnik.Any(e => e.Id == id);
        }

        private bool KorisnikExists(string userName, int? id = null)
        {
            if (id != null)
            {
                return _context.Korisnik.Any(e => e.KorisnickoIme.ToLower().Equals(userName.ToLower()) && e.Id != id.Value);
            }
            else
            {
                return _context.Korisnik.Any(e => e.KorisnickoIme.ToLower().Equals(userName.ToLower()));
            }
        }

        private bool KorisnikExistsByEmail(string email, int? id = null)
        {
            if (id != null)
            {
                return _context.Korisnik.Any(e => e.Email.ToLower().Equals(email.ToLower()) && e.Id != id.Value);
            }
            else
            {
                return _context.Korisnik.Any(e => e.Email.ToLower().Equals(email.ToLower()));
            }
        }
    }
}
