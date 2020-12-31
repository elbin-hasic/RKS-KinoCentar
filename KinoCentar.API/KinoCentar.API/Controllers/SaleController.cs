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
    public class SaleController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public SaleController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSala()
        {
            return await _context.Sala.ToListAsync();
        }

        // GET: api/Sale/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSala(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Sala.ToListAsync();
            }
            else
            {
                return await _context.Sala.Where(x => x.Naziv.Contains(name)).ToListAsync();
            }
        }

        // GET: api/Sale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
            var sala = await _context.Sala.FindAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

        // PUT: api/Sale/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(int id, Sala sala)
        {
            if (id != sala.Id)
            {
                return BadRequest();
            }

            if (SalaExists(sala.Naziv, sala.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.sala_err);
            }

            _context.Entry(sala).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalaExists(id))
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

        // POST: api/Sale
        [HttpPost]
        public async Task<ActionResult<Sala>> PostSala(Sala sala)
        {
            if (SalaExists(sala.Naziv))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.sala_err);
            }

            _context.Sala.Add(sala);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSala", new { id = sala.Id }, sala);
        }

        // DELETE: api/Sale/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sala>> DeleteSala(int id)
        {
            var sala = await _context.Sala.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }

            try
            {
                _context.Sala.Remove(sala);
                await _context.SaveChangesAsync();

                return sala;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool SalaExists(int id)
        {
            return _context.Sala.Any(e => e.Id == id);
        }

        private bool SalaExists(string name, int? id = null)
        {
            if (id != null)
            {
                return _context.Sala.Any(e => e.Naziv.ToLower().Equals(name.ToLower()) && e.Id != id.Value);
            }
            else
            {
                return _context.Sala.Any(e => e.Naziv.ToLower().Equals(name.ToLower()));
            }
        }
    }
}
