using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KinoCentar.API.EntityModels;
using Microsoft.AspNetCore.Authorization;
using KinoCentar.Shared.Extensions;
using System.Net;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ObavijestiController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public ObavijestiController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Obavijesti
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obavijest>>> GetObavijest()
        {
            return await _context.Obavijest.Include(x => x.Korisnik).AsNoTracking().ToListAsync();
        }

        // GET: api/Obavijesti/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Obavijest>>> GetObavijest(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Obavijest.Include(x => x.Korisnik).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Obavijest.Where(x => x.Naslov.Contains(name)).Include(x => x.Korisnik).AsNoTracking().ToListAsync();
            }
        }

        // GET: api/Obavijesti/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Obavijest>> GetObavijest(int id)
        {
            var obavijest = await _context.Obavijest.FindAsync(id);

            if (obavijest == null)
            {
                return NotFound();
            }

            return obavijest;
        }

        // PUT: api/Obavijesti/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObavijest(int id, Obavijest obavijest)
        {
            if (id != obavijest.Id)
            {
                return BadRequest();
            }

            _context.Entry(obavijest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObavijestExists(id))
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

        // POST: api/Obavijesti
        [HttpPost]
        public async Task<ActionResult<Obavijest>> PostObavijest(Obavijest obavijest)
        {
            _context.Obavijest.Add(obavijest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObavijest", new { id = obavijest.Id }, obavijest);
        }

        // DELETE: api/Obavijesti/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Obavijest>> DeleteObavijest(int id)
        {
            var obavijest = await _context.Obavijest.FindAsync(id);
            if (obavijest == null)
            {
                return NotFound();
            }

            try
            {
                _context.Obavijest.Remove(obavijest);
                await _context.SaveChangesAsync();

                return obavijest;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool ObavijestExists(int id)
        {
            return _context.Obavijest.Any(e => e.Id == id);
        }
    }
}
