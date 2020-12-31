using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using KinoCentar.API.EntityModels;
using KinoCentar.Shared;
using KinoCentar.Shared.Extensions;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DojmoviController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public DojmoviController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Dojmovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dojam>>> GetDojam()
        {
            return await _context.Dojam
                        .Include(x => x.Korisnik).AsNoTracking()
                        .Include(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                        .ToListAsync();
        }

        // GET: api/Dojmovi/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Dojam>>> GetDojam(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Dojam
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .ToListAsync();
            }
            else
            {
                return await _context.Dojam.Where(x => x.Projekcija.Film.Naslov.Contains(name))
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.Projekcija).ThenInclude(x => x.Film).AsNoTracking()
                            .ToListAsync();
            }
        }

        // GET: api/Dojmovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dojam>> GetDojam(int id)
        {
            var dojam = await _context.Dojam.FindAsync(id);

            if (dojam == null)
            {
                return NotFound();
            }

            return dojam;
        }

        // GET: api/Dojmovi/GetByUserImpression/{projectionId}/{userName}
        [HttpGet]
        [Route("GetByUserImpression/{projectionId}/{userName}")]
        public async Task<ActionResult<Dojam>> GetDojam(int projectionId, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest();
            }

            Dojam dojam = null;

            var korisnik = await _context.Korisnik
                                 .FirstOrDefaultAsync(x => x.KorisnickoIme.ToLower().Equals(userName.ToLower()));

            if (korisnik != null)
            {
                dojam = await _context.Dojam.FirstOrDefaultAsync(x => x.KorisnikId == korisnik.Id &&
                                                                      x.ProjekcijaId == projectionId);
            }
 
            if (dojam == null)
            {
                return NotFound();
            }

            return dojam;
        }

        // PUT: api/Dojmovi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDojam(int id, Dojam dojam)
        {
            if (id != dojam.Id)
            {
                return BadRequest();
            }

            _context.Entry(dojam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DojamExists(id))
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

        // POST: api/Dojmovi
        [HttpPost]
        public async Task<ActionResult<Dojam>> PostDojam(Dojam dojam)
        {
            _context.Dojam.Add(dojam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDojam", new { id = dojam.Id }, dojam);
        }

        // DELETE: api/Dojmovi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dojam>> DeleteDojam(int id)
        {
            var dojam = await _context.Dojam.FindAsync(id);
            if (dojam == null)
            {
                return NotFound();
            }

            try
            {
                _context.Dojam.Remove(dojam);
                await _context.SaveChangesAsync();

                return dojam;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool DojamExists(int id)
        {
            return _context.Dojam.Any(e => e.Id == id);
        }
    }
}
