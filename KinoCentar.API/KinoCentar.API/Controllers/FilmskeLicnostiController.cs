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
    public class FilmskeLicnostiController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public FilmskeLicnostiController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/FilmskeLicnosti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmskaLicnost>>> GetFilmskaLicnost()
        {
            return await _context.FilmskaLicnost.ToListAsync();
        }

        // GET: api/FilmskeLicnosti/SearchByName/{firstName}/{lastName}
        [HttpGet]
        [Route("SearchByName/{firstName}/{lastName}")]
        public async Task<ActionResult<IEnumerable<FilmskaLicnost>>> GetFilmskaLicnost(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && firstName != "*" && !string.IsNullOrEmpty(lastName) && lastName != "*")
            {                
                return await _context.FilmskaLicnost.Where(x => x.Ime.Contains(firstName) || x.Prezime.Contains(lastName)).ToListAsync();
            }
            else if(!string.IsNullOrEmpty(firstName) && firstName != "*")
            {
                return await _context.FilmskaLicnost.Where(x => x.Ime.Contains(firstName)).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(lastName) && lastName != "*")
            {
                return await _context.FilmskaLicnost.Where(x => x.Prezime.Contains(lastName)).ToListAsync();
            }
            else
            {
                return await _context.FilmskaLicnost.ToListAsync();
            }
        }

        // GET: api/FilmskeLicnosti/GetByType/{isGlumac}/{isReziser}
        [HttpGet]
        [Route("GetByType/{isGlumac}/{isReziser}")]
        public async Task<ActionResult<IEnumerable<FilmskaLicnost>>> GetFilmskaLicnost(bool isGlumac, bool isReziser)
        {
            if (isGlumac && isReziser)
            {
                return await _context.FilmskaLicnost.Where(x => x.IsGlumac == true && x.IsReziser == true).ToListAsync();
            }
            else if (isGlumac)
            {
                return await _context.FilmskaLicnost.Where(x => x.IsGlumac == true).ToListAsync();
            }
            else if (isReziser)
            {
                return await _context.FilmskaLicnost.Where(x => x.IsReziser == true).ToListAsync();
            }
            else
            {
                return await _context.FilmskaLicnost.Where(x => x.IsGlumac == false && x.IsReziser == false).ToListAsync();
            }
        }

        // GET: api/FilmskeLicnosti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmskaLicnost>> GetFilmskaLicnost(int id)
        {
            var filmskaLicnost = await _context.FilmskaLicnost.FindAsync(id);

            if (filmskaLicnost == null)
            {
                return NotFound();
            }

            return filmskaLicnost;
        }

        // PUT: api/FilmskeLicnosti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmskaLicnost(int id, FilmskaLicnost filmskaLicnost)
        {
            if (id != filmskaLicnost.Id)
            {
                return BadRequest();
            }

            if (FilmskaLicnostExists(filmskaLicnost.Ime, filmskaLicnost.Prezime, filmskaLicnost.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.filmskaLicnost_err);
            }

            _context.Entry(filmskaLicnost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmskaLicnostExists(id))
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

        // POST: api/FilmskeLicnosti
        [HttpPost]
        public async Task<ActionResult<FilmskaLicnost>> PostFilmskaLicnost(FilmskaLicnost filmskaLicnost)
        {
            if (FilmskaLicnostExists(filmskaLicnost.Ime, filmskaLicnost.Prezime))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.filmskaLicnost_err);
            }

            _context.FilmskaLicnost.Add(filmskaLicnost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmskaLicnost", new { id = filmskaLicnost.Id }, filmskaLicnost);
        }

        // DELETE: api/FilmskeLicnosti/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FilmskaLicnost>> DeleteFilmskaLicnost(int id)
        {
            var filmskaLicnost = await _context.FilmskaLicnost.FindAsync(id);
            if (filmskaLicnost == null)
            {
                return NotFound();
            }

            try
            {
                _context.FilmskaLicnost.Remove(filmskaLicnost);
                await _context.SaveChangesAsync();

                return filmskaLicnost;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool FilmskaLicnostExists(int id)
        {
            return _context.FilmskaLicnost.Any(e => e.Id == id);
        }

        private bool FilmskaLicnostExists(string name, string lastName, int? id = null)
        {
            if (id != null)
            {
                return _context.FilmskaLicnost.Any(e => e.Ime.ToLower().Equals(name.ToLower()) &&
                                                        e.Prezime.ToLower().Equals(lastName.ToLower()) &&
                                                        e.Id != id.Value);
            }
            else
            {
                return _context.FilmskaLicnost.Any(e => e.Ime.ToLower().Equals(name.ToLower()) &&
                                                        e.Prezime.ToLower().Equals(lastName.ToLower()));
            }
        }
    }
}
