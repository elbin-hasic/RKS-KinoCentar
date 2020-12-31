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
using KinoCentar.Shared;
using KinoCentar.Shared.Extensions;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjekcijeController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;
        private readonly int _recommendedMaxListCount = 5;

        public ProjekcijeController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Projekcije
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projekcija>>> GetProjekcija()
        {
            return await _context.Projekcija
                        .Include(x => x.Termini).AsNoTracking()
                        .Include(x => x.Film).AsNoTracking()
                        .Include(x => x.Sala).AsNoTracking()
                        .ToListAsync();
        }

        // GET: api/Projekcije/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Projekcija>>> GetProjekcija(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Projekcija
                            .Include(x => x.Termini).AsNoTracking()
                            .Include(x => x.Film).AsNoTracking()
                            .Include(x => x.Sala).AsNoTracking()
                            .ToListAsync();
            }
            else
            {
                return await _context.Projekcija
                            .Include(x => x.Termini).AsNoTracking()
                            .Include(x => x.Film).AsNoTracking()
                            .Include(x => x.Sala).AsNoTracking()
                            .Where(x => x.Film.Naslov.Contains(name)).ToListAsync();
            }
        }

        // GET: api/Projekcije/ActiveList
        [HttpGet]
        [Route("ActiveList")]
        public async Task<ActionResult<IEnumerable<Projekcija>>> GetAktivneProjekcije()
        {
            var dtn = DateTime.Now.Date;
            return await _context.Projekcija
                            .Include(x => x.Termini).AsNoTracking()
                            .Include(x => x.Film).AsNoTracking()
                            .Include(x => x.Sala).AsNoTracking()
                            .Where(x => x.VrijediOd.Date <= dtn && x.VrijediDo.Date >= dtn).ToListAsync();
        }

        // GET: api/Projekcije/RecommendedList/{userName}
        [HttpGet]
        [Route("RecommendedList/{userName}")]
        public async Task<ActionResult<IEnumerable<Projekcija>>> GetPreporuceneProjekcije(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            var dtn = DateTime.Now.Date;

            var recommendedProjekcije = new List<Projekcija>();

            var korisnik = await _context.Korisnik.FirstOrDefaultAsync(x => x.KorisnickoIme.ToLower().Equals(userName.ToLower()));
            if (korisnik != null)
            {
                var recommendedRediteljiIds = new List<int>();
                var recommendedZanroviIds = new List<int>();
                var posjeceneProjekcijeIds = new List<int>();

                var posjeceneProjekcije = await _context.ProjekcijaKorisnikDodjela
                                                        .Include(x => x.Projekcija)
                                                            .ThenInclude(y => y.Film)
                                                        .Where(x => x.KorisnikId == korisnik.Id)
                                                        .Select(x => x.Projekcija).ToListAsync();

                recommendedRediteljiIds.AddRange(posjeceneProjekcije.Where(x => x.Film.RediteljId != null)
                                                                    .Select(x => x.Film.RediteljId.Value));
                recommendedZanroviIds.AddRange(posjeceneProjekcije.Where(x => x.Film.ZanrId != null)
                                                                    .Select(x => x.Film.ZanrId.Value));

                posjeceneProjekcijeIds.AddRange(posjeceneProjekcije.Select(x => x.Id));

                if (recommendedRediteljiIds.Any() || recommendedZanroviIds.Any())
                {
                    if (recommendedRediteljiIds.Any() && recommendedZanroviIds.Any())
                    {
                        recommendedProjekcije = await _context.Projekcija
                                                        .Include(x => x.Termini).AsNoTracking()
                                                        .Include(x => x.Film).AsNoTracking()
                                                        .Include(x => x.Sala).AsNoTracking()
                                                        .Where(x => x.VrijediOd.Date <= dtn && x.VrijediDo.Date >= dtn &&
                                                                    !posjeceneProjekcijeIds.Contains(x.Id) &&
                                                                    ((x.Film.RediteljId != null && recommendedRediteljiIds.Contains(x.Film.RediteljId.Value)) ||
                                                                    (x.Film.ZanrId != null && recommendedZanroviIds.Contains(x.Film.ZanrId.Value))))
                                                        .OrderBy(x => Guid.NewGuid()).Take(_recommendedMaxListCount)
                                                        .ToListAsync();
                    }
                    else if (recommendedRediteljiIds.Any())
                    {
                        recommendedProjekcije = await _context.Projekcija
                                                        .Include(x => x.Termini).AsNoTracking()
                                                        .Include(x => x.Film).AsNoTracking()
                                                        .Include(x => x.Sala).AsNoTracking()
                                                        .Where(x => x.VrijediOd.Date <= dtn && x.VrijediDo.Date >= dtn &&
                                                                    !posjeceneProjekcijeIds.Contains(x.Id) &&
                                                                    x.Film.RediteljId != null && recommendedRediteljiIds.Contains(x.Film.RediteljId.Value))
                                                        .OrderBy(x => Guid.NewGuid()).Take(_recommendedMaxListCount)
                                                        .ToListAsync();
                    }
                    else
                    {
                        recommendedProjekcije = await _context.Projekcija
                                                        .Include(x => x.Termini).AsNoTracking()
                                                        .Include(x => x.Film).AsNoTracking()
                                                        .Include(x => x.Sala).AsNoTracking()
                                                        .Where(x => x.VrijediOd.Date <= dtn && x.VrijediDo.Date >= dtn &&
                                                                    !posjeceneProjekcijeIds.Contains(x.Id) &&
                                                                    x.Film.ZanrId != null && recommendedZanroviIds.Contains(x.Film.ZanrId.Value))
                                                        .OrderBy(x => Guid.NewGuid()).Take(_recommendedMaxListCount)
                                                        .ToListAsync();
                    }
                }
            }

            if (recommendedProjekcije.Any())
            {
                return recommendedProjekcije;
            }
            else
            {
                return await _context.Projekcija
                            .Include(x => x.Termini).AsNoTracking()
                            .Include(x => x.Film).AsNoTracking()
                            .Include(x => x.Sala).AsNoTracking()
                            .Where(x => x.VrijediOd.Date <= dtn && x.VrijediDo.Date >= dtn)
                            .OrderBy(x => Guid.NewGuid()).Take(_recommendedMaxListCount)
                            .ToListAsync();
            }
        }

        // GET: api/Projekcije/Terms/{projekcijaId}
        [HttpGet]
        [Route("Terms/{projekcijaId}")]
        public async Task<ActionResult<IEnumerable<ProjekcijaTermin>>> GetRezervacijaTerms(int projekcijaId)
        {
            var projekcija = await _context.Projekcija
                                    .Include(x => x.Termini).AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.Id == projekcijaId);
            if (projekcija == null)
            {
                return NotFound();
            }

            var termini = projekcija.Termini.ToList();

            return termini;
        }

        // GET: api/Projekcije/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projekcija>> GetProjekcija(int id)
        {
            var projekcija = await _context.Projekcija
                                           .Include(x => x.Termini).AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (projekcija == null)
            {
                return NotFound();
            }

            return projekcija;
        }

        // PUT: api/Projekcije/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjekcija(int id, Projekcija projekcija)
        {
            if (id != projekcija.Id)
            {
                return BadRequest();
            }

            if (ProjekcijaExists(projekcija.FilmId, projekcija.VrijediOd, projekcija.VrijediDo, projekcija.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.projekcija_err);
            }

            if (projekcija.Termini != null && projekcija.Termini.Any())
            {
                foreach (var termin in projekcija.Termini)
                {
                    if (termin.Id == 0)
                    {
                        _context.ProjekcijaTermin.Add(termin);
                    }
                    else
                    {
                        _context.Entry(termin).State = EntityState.Modified;
                    }
                }
            }

            _context.Entry(projekcija).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjekcijaExists(id))
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

        // POST: api/Projekcije
        [HttpPost]
        public async Task<ActionResult<Projekcija>> PostProjekcija(Projekcija projekcija)
        {
            if (ProjekcijaExists(projekcija.FilmId, projekcija.VrijediOd, projekcija.VrijediDo))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.projekcija_err);
            }

            _context.Projekcija.Add(projekcija);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjekcija", new { id = projekcija.Id }, projekcija);
        }

        // PUT: api/Projekcije/Visit/{projekcijaId}/{korisnikId}
        [HttpPut]
        [Route("Visit/{projekcijaId}/{korisnikId}")]
        public async Task<IActionResult> VisitProjekcija(int projekcijaId, int korisnikId)
        {
            var projekcija = await _context.Projekcija.FindAsync(projekcijaId);
            if (projekcija == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik.FindAsync(korisnikId);
            if (korisnik == null)
            {
                return NotFound();
            }

            var dtn = DateTime.Now;

            var projekcijaKorisnik = await _context.ProjekcijaKorisnikDodjela.FirstOrDefaultAsync(x => x.ProjekcijaId == projekcijaId && x.KorisnikId == korisnikId);
            if (projekcijaKorisnik == null)
            {
                projekcijaKorisnik = new ProjekcijaKorisnikDodjela()
                {
                    DatumPosjete = dtn,
                    ProjekcijaId = projekcijaId,
                    KorisnikId = korisnikId
                };                
                _context.ProjekcijaKorisnikDodjela.Add(projekcijaKorisnik);
            }

            projekcijaKorisnik.DatumPosljednjePosjete = dtn;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Projekcije/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Projekcija>> DeleteProjekcija(int id)
        {
            var projekcija = await _context.Projekcija.FindAsync(id);
            if (projekcija == null)
            {
                return NotFound();
            }

            try
            {
                _context.Projekcija.Remove(projekcija);
                await _context.SaveChangesAsync();

                return projekcija;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool ProjekcijaExists(int id)
        {
            return _context.Projekcija.Any(e => e.Id == id);
        }

        private bool ProjekcijaExists(int filmId, DateTime vrijediOd, DateTime vrijediDo, int? id = null)
        {
            if (id != null)
            {
                return _context.Projekcija.Any(e => e.FilmId == filmId &&
                                                    ((vrijediOd.Date >= e.VrijediOd.Date && vrijediOd.Date <= e.VrijediDo.Date) ||
                                                     (vrijediDo.Date >= e.VrijediOd.Date && vrijediDo.Date <= e.VrijediDo.Date)) && 
                                                     e.Id != id.Value);
            }
            else
            {
                return _context.Projekcija.Any(e => e.FilmId == filmId && 
                                                    ((vrijediOd.Date >= e.VrijediOd.Date && vrijediOd.Date <= e.VrijediDo.Date) ||
                                                     (vrijediDo.Date >= e.VrijediOd.Date && vrijediDo.Date <= e.VrijediDo.Date)));
            }
        }
    }
}
