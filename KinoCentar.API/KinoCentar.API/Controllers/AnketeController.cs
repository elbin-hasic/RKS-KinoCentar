using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KinoCentar.API.EntityModels;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using KinoCentar.API.EntityModels.Extensions;
using KinoCentar.Shared;
using KinoCentar.Shared.Extensions;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnketeController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public AnketeController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Ankete
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anketa>>> GetAnketa()
        {
            return await _context.Anketa
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.Odgovori).AsNoTracking()
                            .ToListAsync();
        }

        // GET: api/Ankete/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Anketa>>> GetAnketa(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Anketa
                                .Include(x => x.Korisnik).AsNoTracking()
                                .Include(x => x.Odgovori).AsNoTracking()
                                .ToListAsync();
            }
            else
            {
                return await _context.Anketa
                                .Include(x => x.Korisnik).AsNoTracking()
                                .Include(x => x.Odgovori).AsNoTracking()
                                .Where(x => x.Naslov.Contains(name)).ToListAsync();
            }
        }

        // GET: api/Ankete/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anketa>> GetAnketa(int id)
        {
            var anketa = await _context.Anketa
                                        .Include(x => x.Korisnik).AsNoTracking()
                                        .Include(x => x.Odgovori).AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (anketa == null)
            {
                return NotFound();
            }

            return anketa;
        }

        // GET: api/Ankete/AnketaForUser/{id}/{korisnikId}
        [HttpGet("AnketaForUser/{id}/{korisnikId}")]
        public async Task<ActionResult<AnketaExtension>> GetAnketaForUser(int id, int korisnikId)
        {
            var anketa = await _context.Anketa
                                        .Include(x => x.Korisnik).AsNoTracking()
                                        .Include(x => x.Odgovori).AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == id);

            if (anketa == null)
            {
                return NotFound();
            }

            var anketaEx = GetAnketaExtension(anketa, korisnikId);

            return anketaEx;
        }

        // GET: api/Active/{korisnikId}
        [HttpGet("Active/{korisnikId}")]
        public async Task<ActionResult<AnketaExtension>> GetActiveAnketa(int korisnikId)
        {
            var anketa = await _context.Anketa
                                    .Include(x => x.Korisnik).AsNoTracking()
                                    .Include(x => x.Odgovori).AsNoTracking()
                                .Where(x => x.ZakljucenoDatum == null)
                                .OrderByDescending(x => x.Id)
                                .FirstOrDefaultAsync();

            if (anketa == null)
            {
                return NotFound();
            }

            var anketaEx = GetAnketaExtension(anketa, korisnikId);

            return anketaEx;
        }

        // PUT: api/Ankete/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnketa(int id, Anketa anketa)
        {
            if (id != anketa.Id)
            {
                return BadRequest();
            }

            if (anketa.Odgovori != null)
            {
                foreach (var odgovor in anketa.Odgovori)
                {
                    if (odgovor.Id == 0)
                    {
                        _context.AnketaOdgovor.Add(odgovor);
                    }
                    else
                    {
                        _context.Entry(odgovor).State = EntityState.Modified;
                    }
                }
            }

            _context.Entry(anketa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnketaExists(id))
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

        // POST: api/Ankete
        [HttpPost]
        public async Task<ActionResult<Anketa>> PostAnketa(Anketa anketa)
        {
            if (anketa.Odgovori != null)
            {
                foreach (var odgovor in anketa.Odgovori)
                {
                    odgovor.Anketa = anketa;
                    _context.AnketaOdgovor.Add(odgovor);
                }
            }

            var dtn = DateTime.Now;

            var ankete = await _context.Anketa.Where(x => x.ZakljucenoDatum == null).ToListAsync();
            foreach (var item in ankete)
            {
                item.ZakljucenoDatum = dtn;
            }

            _context.Anketa.Add(anketa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnketa", new { id = anketa.Id }, anketa);
        }

        // POST: api/Ankete/UserAnswer
        [HttpPost]
        [Route("UserAnswer")]
        public async Task<ActionResult<AnketaExtension>> PostAnketaKorisnikOdgovor(AnketaOdgovorKorisnikDodjela anketaKorisnikOdgovor)
        {
            if (AnketaKorisnikOdgovorExists(anketaKorisnikOdgovor.KorisnikId, anketaKorisnikOdgovor.AnketaOdgovorId))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.anketaKorisnikOdgovor_err);
            }

            var anketaOdgovor = await _context.AnketaOdgovor.FirstOrDefaultAsync(x => x.Id == anketaKorisnikOdgovor.AnketaOdgovorId);
            if (anketaOdgovor == null)
            {
                return NotFound();
            }

            anketaOdgovor.UkupnoIzabrano = anketaOdgovor.UkupnoIzabrano + 1;

            var anketa = await _context.Anketa
                                        .Include(x => x.Korisnik).AsNoTracking()
                                        .Include(x => x.Odgovori).AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == anketaOdgovor.AnketaId);
            if (anketa == null)
            {
                return NotFound();
            }

            _context.AnketaOdgovorKorisnikDodjela.Add(anketaKorisnikOdgovor);
            await _context.SaveChangesAsync();

            var anketaEx = GetAnketaExtension(anketa, anketaKorisnikOdgovor.KorisnikId);

            return CreatedAtAction("GetAnketaForUser", new { id = anketa.Id, korisnikId = anketaKorisnikOdgovor.KorisnikId }, anketaEx);
        }

        // PUT: api/Ankete/5
        [HttpPut]
        [Route("Close/{id}")]
        public async Task<ActionResult<Anketa>> CloseRezervacija(int id)
        {
            var anketa = await _context.Anketa.FindAsync(id);
            if (anketa == null)
            {
                return NotFound();
            }

            if (anketa.ZakljucenoDatum != null)
            {
                return StatusCode((int)HttpStatusCode.Conflict, "Navedena anketa je već zaklučana!");
            }

            anketa.ZakljucenoDatum = DateTime.Now;
            await _context.SaveChangesAsync();

            return anketa;
        }

        // DELETE: api/Ankete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Anketa>> DeleteAnketa(int id)
        {
            var anketa = await _context.Anketa.FindAsync(id);
            if (anketa == null)
            {
                return NotFound();
            }

            try
            {
                _context.Anketa.Remove(anketa);
                await _context.SaveChangesAsync();
                return anketa;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private AnketaExtension GetAnketaExtension(Anketa anketa, int korisnikId)
        {
            var anketaEx = new AnketaExtension(anketa);

            var korisnikOdgovor = _context.AnketaOdgovorKorisnikDodjela
                                            .Include(x => x.AnketaOdgovor).AsNoTracking()
                                        .Where(x => x.AnketaOdgovor.AnketaId == anketaEx.Id &&
                                                    x.KorisnikId == korisnikId)
                                        .FirstOrDefault();
            if (korisnikOdgovor != null)
            {
                anketaEx.KorisnikAnketaOdgovorId = korisnikOdgovor.AnketaOdgovorId;
                anketaEx.KorisnikAnketaOdgovor = korisnikOdgovor.AnketaOdgovor;
            }

            return anketaEx;
        }

        private bool AnketaExists(int id)
        {
            return _context.Anketa.Any(e => e.Id == id);
        }

        private bool AnketaKorisnikOdgovorExists(int korisnikId, int anketaOdgovorId)
        {
            return _context.AnketaOdgovorKorisnikDodjela.Any(e => e.KorisnikId == korisnikId && 
                                                                  e.AnketaOdgovorId == anketaOdgovorId);
        }
    }
}
