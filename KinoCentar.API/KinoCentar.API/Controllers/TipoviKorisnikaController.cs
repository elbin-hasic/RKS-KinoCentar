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
    public class TipoviKorisnikaController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public TipoviKorisnikaController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoviKorisnika
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipKorisnika>>> GetTipKorisnika()
        {
            return await _context.TipKorisnika.ToListAsync();
        }


        // GET: api/TipoviKorisnika/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<TipKorisnika>>> GetTipKorisnika(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.TipKorisnika.ToListAsync();
            }
            else
            {
                return await _context.TipKorisnika.Where(x => x.Naziv.Contains(name)).ToListAsync();
            }
        }

        // GET: api/TipoviKorisnika/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipKorisnika>> GetTipKorisnika(int id)
        {
            var tipKorisnika = await _context.TipKorisnika.FindAsync(id);

            if (tipKorisnika == null)
            {
                return NotFound();
            }

            return tipKorisnika;
        }

        // PUT: api/TipoviKorisnika/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipKorisnika(int id, TipKorisnika tipKorisnika)
        {
            if (id != tipKorisnika.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipKorisnika).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipKorisnikaExists(id))
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

        // POST: api/TipoviKorisnika
        [HttpPost]
        public async Task<ActionResult<TipKorisnika>> PostTipKorisnika(TipKorisnika tipKorisnika)
        {
            _context.TipKorisnika.Add(tipKorisnika);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipKorisnika", new { id = tipKorisnika.Id }, tipKorisnika);
        }

        // DELETE: api/TipoviKorisnika/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipKorisnika>> DeleteTipKorisnika(int id)
        {
            var tipKorisnika = await _context.TipKorisnika.FindAsync(id);
            if (tipKorisnika == null)
            {
                return NotFound();
            }

            try
            {
                _context.TipKorisnika.Remove(tipKorisnika);
                await _context.SaveChangesAsync();

                return tipKorisnika;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool TipKorisnikaExists(int id)
        {
            return _context.TipKorisnika.Any(e => e.Id == id);
        }
    }
}
