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

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikliController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public ArtikliController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Artikli
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artikal>>> GetArtikal()
        {
            return await _context.Artikal.Include(x => x.JedinicaMjere).AsNoTracking().ToListAsync();
        }

        // GET: api/Artikli/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Artikal>>> GetArtikal(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Artikal.Include(x => x.JedinicaMjere).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Artikal.Include(x => x.JedinicaMjere).AsNoTracking().Where(x => x.Naziv.Contains(name)).ToListAsync();
            }
        }

        // GET: api/Artikli/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artikal>> GetArtikal(int id)
        {
            var artikal = await _context.Artikal.FindAsync(id);

            if (artikal == null)
            {
                return NotFound();
            }

            return artikal;
        }

        // PUT: api/Artikli/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtikal(int id, Artikal artikal)
        {
            if (id != artikal.Id)
            {
                return BadRequest();
            }

            if (ArtikalExists(artikal.Naziv, artikal.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.artikal_err);
            }

            _context.Entry(artikal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikalExists(id))
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

        // POST: api/Artikli
        [HttpPost]
        public async Task<ActionResult<Artikal>> PostArtikal(Artikal artikal)
        {
            if (string.IsNullOrEmpty(artikal.Naziv))
            {
                return BadRequest();
            }

            if (ArtikalExists(artikal.Naziv))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.artikal_err);
            }

            artikal.Sifra = GenerateSifra();
            if (string.IsNullOrEmpty(artikal.Sifra))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.artikal_sifra_err);
            }

            _context.Artikal.Add(artikal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtikal", new { id = artikal.Id }, artikal);
        }

        // DELETE: api/Artikli/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artikal>> DeleteArtikal(int id)
        {
            var artikal = await _context.Artikal.FindAsync(id);
            if (artikal == null)
            {
                return NotFound();
            }

            try
            {
                _context.Artikal.Remove(artikal);
                await _context.SaveChangesAsync();

                return artikal;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool ArtikalExists(int id)
        {
            return _context.Artikal.Any(e => e.Id == id);
        }

        private bool ArtikalExists(string name, int? id = null)
        {
            if (id != null)
            {
                return _context.Artikal.Any(e => e.Naziv.ToLower().Equals(name.ToLower()) && e.Id != id.Value);
            }
            else
            {
                return _context.Artikal.Any(e => e.Naziv.ToLower().Equals(name.ToLower()));
            }
        }

        private string GenerateSifra()
        {
            bool isSifraOk = false;
            int i = 0;

            string sifra = string.Empty;

            var lastArtikal = _context.Artikal.OrderByDescending(x => x.Id).FirstOrDefault();

            int sifra_br = 1;

            do
            {
                if (lastArtikal != null)
                {
                    try
                    {
                        sifra_br = int.Parse(lastArtikal.Sifra);
                        sifra_br++;
                    }
                    catch
                    { }
                }

                sifra = String.Format("{0:D6}", sifra_br);

                isSifraOk = !_context.Artikal.Any(e => e.Sifra.ToLower().Equals(sifra.ToLower()));
                i++;
            } while (!isSifraOk && i < 5);

            if (!isSifraOk)
            {
                sifra = string.Empty;
            }

            return sifra;
        }
    }
}
