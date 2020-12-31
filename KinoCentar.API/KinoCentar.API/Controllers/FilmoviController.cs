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
    public class FilmoviController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public FilmoviController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Filmovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilm()
        {
            return await _context.Film
                            .Include(x => x.Reditelj).AsNoTracking()
                            .Include(x => x.Zanr).AsNoTracking()
                         .ToListAsync();
        }

        // GET: api/Filmovi/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilm(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Film
                                .Include(x => x.Reditelj).AsNoTracking()
                                .Include(x => x.Zanr).AsNoTracking()
                             .ToListAsync();
            }
            else
            {
                return await _context.Film.Where(x => x.Naslov.Contains(name))
                                .Include(x => x.Reditelj).AsNoTracking()
                                .Include(x => x.Zanr).AsNoTracking()
                             .ToListAsync();
            }
        }

        // GET: api/Filmovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilm(int id)
        {
            var film = await _context.Film.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }

        // PUT: api/Filmovi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.Id)
            {
                return BadRequest();
            }

            if (FilmExists(film.Naslov, film.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.film_err);
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
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

        // POST: api/Filmovi
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            if (FilmExists(film.Naslov))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.film_err);
            }

            _context.Film.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.Id }, film);
        }

        // DELETE: api/Filmovi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Film>> DeleteFilm(int id)
        {
            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            try
            {
                _context.Film.Remove(film);
                await _context.SaveChangesAsync();

                return film;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }

        private bool FilmExists(string name, int? id = null)
        {
            if (id != null)
            {
                return _context.Film.Any(e => e.Naslov.ToLower().Equals(name.ToLower()) && e.Id != id.Value);
            }
            else
            {
                return _context.Film.Any(e => e.Naslov.ToLower().Equals(name.ToLower()));
            }
        }
    }
}
