using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KinoCentar.API.EntityModels;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using KinoCentar.Shared;
using KinoCentar.Shared.Extensions;
using KinoCentar.API.EntityModels.Extensions;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RezervacijeController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public RezervacijeController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Rezervacije
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> GetRezervacija()
        {
            return await _context.Rezervacija
                        .Include(x => x.Korisnik).AsNoTracking()
                        .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                        .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                        .ToListAsync();
        }

        // GET: api/Rezervacije/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> GetRezervacija(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Rezervacija
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .ToListAsync();
            }
            else
            {
                return await _context.Rezervacija.Where(x => x.ProjekcijaTermin.Projekcija.Film.Naslov.Contains(name))
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .ToListAsync();
            }
        }

        // GET: api/Rezervacije/GetByUserName/{userName}
        [HttpGet]
        [Route("GetByUserName/{userName}")]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> GetRezervacijePoKorisniku(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            return await _context.Rezervacija
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .Where(x => x.Korisnik.KorisnickoIme.ToLower() == userName.ToLower())
                            .ToListAsync();
        }

        // GET: api/Rezervacije/GetByType/{isProdano}/{isOtkazano}
        [HttpGet]
        [Route("GetByType/{isProdano}/{isOtkazano}")]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> GetRezervacija(bool isProdano, bool isOtkazano)
        {
            if (isProdano && isOtkazano)
            {
                return await _context.Rezervacija
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .Where(x => x.DatumProdano != null && x.DatumOtkazano != null)
                            .ToListAsync();
            }
            else if (isProdano)
            {
                return await _context.Rezervacija
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .Where(x => x.DatumProdano != null)
                            .ToListAsync();
            }
            else if (isOtkazano)
            {
                return await _context.Rezervacija
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .Where(x => x.DatumOtkazano != null)
                            .ToListAsync();
            }
            else
            {
                return await _context.Rezervacija
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .Include(x => x.ProjekcijaTermin).ThenInclude(x => x.Projekcija).ThenInclude(x => x.Sala).AsNoTracking()
                            .Where(x => x.DatumProdano == null && x.DatumOtkazano == null)
                            .ToListAsync();
            }
        }

        // GET: api/Rezervacije/FreeSeats/{projekcijaId}/{rezervacijaId?}
        [HttpGet]
        [Route("FreeSeats/{projekcijaId}/{rezervacijaId?}")]
        public async Task<ActionResult<IEnumerable<int>>> GetRezervacijaSeats(int projekcijaId, int? rezervacijaId = null)
        {
            var projekcija = await _context.Projekcija
                                    .Include(x => x.Sala).AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == projekcijaId);
            if (projekcija == null)
            {
                return NotFound();
            }

            var brojeviSjedista = new List<int>();
            if (projekcija.Sala?.BrojSjedista != null && projekcija.Sala.BrojSjedista > 0)
            {
                brojeviSjedista = Enumerable.Range(1, projekcija.Sala.BrojSjedista.Value).ToList();
            }
            else
            {
                return NotFound();
            }

            var rezbrojeviSjedista = new List<int>();
            if (rezervacijaId != null)
            {
                rezbrojeviSjedista = await _context.Rezervacija.Where(x => x.ProjekcijaTermin.ProjekcijaId == projekcijaId && x.Id != rezervacijaId.Value)
                                                               .Select(x => x.BrojSjedista).ToListAsync();
            }
            else
            {
                rezbrojeviSjedista = await _context.Rezervacija.Where(x => x.ProjekcijaTermin.ProjekcijaId == projekcijaId)
                                                               .Select(x => x.BrojSjedista).ToListAsync();
            }

            foreach (var rezBroj in rezbrojeviSjedista)
            {
                if (brojeviSjedista.Contains(rezBroj))
                {
                    brojeviSjedista.Remove(rezBroj);
                }
            }

            return brojeviSjedista;
        }

        // GET: api/Rezervacije/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rezervacija>> GetRezervacija(int id)
        {
            var rezervacija = await _context.Rezervacija
                                            .Include(x => x.ProjekcijaTermin)
                                            .FirstOrDefaultAsync(x => x.Id == id);

            if (rezervacija == null)
            {
                return NotFound();
            }

            return rezervacija;
        }

        // GET: api/Rezervacije/GetByUserProjection/{projectionId}/{userName}
        [HttpGet]
        [Route("GetByUserProjection/{projectionId}/{userName}")]
        public async Task<ActionResult<Rezervacija>> GetRezervacija(int projectionId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            Rezervacija rezervacija = null;

            var korisnik = await _context.Korisnik
                                 .FirstOrDefaultAsync(x => x.KorisnickoIme.ToLower().Equals(userName.ToLower()));

            if (korisnik != null)
            {
                rezervacija = await _context.Rezervacija
                                            .Include(x => x.ProjekcijaTermin)
                                            .FirstOrDefaultAsync(x => x.KorisnikId != null &&
                                                              x.KorisnikId == korisnik.Id &&
                                                              x.ProjekcijaTermin.ProjekcijaId == projectionId &&
                                                              x.DatumOtkazano == null && x.DatumProdano == null);
            }

            if (rezervacija == null)
            {
                return NotFound();
            }

            return rezervacija;
        }

        // PUT: api/Rezervacije/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRezervacija(int id, RezervacijaExtension rezervacija)
        {
            if (rezervacija.ProjekcijaId == null || id != rezervacija.Id)
            {
                return BadRequest();
            }

            if (RezervacijaExists(rezervacija.ProjekcijaId.Value, rezervacija.ProjekcijaTerminId, rezervacija.KorisnikId, rezervacija.DatumProjekcije, rezervacija.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.rezervacija_err);
            }

            _context.Entry(rezervacija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RezervacijaExists(id))
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

        // PUT: api/Rezervacije/5
        [HttpPut]
        [Route("Disable/{id}")]
        public async Task<ActionResult<Rezervacija>> DisableRezervacija(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            if (rezervacija.DatumOtkazano != null)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "Navedena rezervacija je već otakazan!");
            }
            else if (rezervacija.DatumProdano != null)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "Navedenu rezervaciju nije moguće otkazati zbog toga što je već prodana!");
            }

            rezervacija.DatumOtkazano = DateTime.Now;
            await _context.SaveChangesAsync();

            return rezervacija;
        }

        // POST: api/Rezervacije
        [HttpPost]
        public async Task<ActionResult<Rezervacija>> PostRezervacija(RezervacijaExtension rezervacija)
        {
            if (rezervacija.ProjekcijaId == null)
            {
                return BadRequest();
            }

            if (RezervacijaExists(rezervacija.ProjekcijaId.Value, rezervacija.ProjekcijaTerminId, rezervacija.KorisnikId, rezervacija.DatumProjekcije))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.rezervacija_err);
            }

            _context.Rezervacija.Add(rezervacija);
            await _context.SaveChangesAsync();

            rezervacija.ProjekcijaTermin = _context.ProjekcijaTermin.FirstOrDefault(x => x.Id == rezervacija.ProjekcijaTerminId);

            return CreatedAtAction("GetRezervacija", new { id = rezervacija.Id }, rezervacija);
        }

        // DELETE: api/Rezervacije/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rezervacija>> DeleteRezervacija(int id)
        {
            var rezervacija = await _context.Rezervacija.FindAsync(id);
            if (rezervacija == null)
            {
                return NotFound();
            }

            try
            {
                _context.Rezervacija.Remove(rezervacija);
                await _context.SaveChangesAsync();

                return rezervacija;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool RezervacijaExists(int id)
        {
            return _context.Rezervacija.Any(e => e.Id == id);
        }

        private bool RezervacijaExists(int projekcijaId, int projekcijaTerminId, int? korisnikId, DateTime datumProjekcije, int? id = null)
        {
            if (korisnikId == null)
            {
                return false;
            }

            if (id != null)
            {
                return _context.Rezervacija.Any(e => e.ProjekcijaTermin.ProjekcijaId == projekcijaId &&
                                                     e.ProjekcijaTerminId == projekcijaTerminId &&
                                                     e.KorisnikId == korisnikId &&
                                                     e.DatumProjekcije.Date == datumProjekcije.Date &&
                                                     e.DatumOtkazano == null && e.DatumProdano == null &&
                                                     e.Id != id.Value);
            }
            else
            {
                return _context.Rezervacija.Any(e => e.ProjekcijaTermin.ProjekcijaId == projekcijaId &&
                                                     e.ProjekcijaTerminId == projekcijaTerminId &&
                                                     e.KorisnikId == korisnikId &&
                                                     e.DatumOtkazano == null && e.DatumProdano == null &&
                                                     e.DatumProjekcije.Date == datumProjekcije.Date);
            }
        }
    }
}
