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
    public class ZanroviController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public ZanroviController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Zanrovi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zanr>>> GetZanr()
        {
            return await _context.Zanr.ToListAsync();
        }

        // GET: api/Zanrovi/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<Zanr>>> GetZanr(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.Zanr.ToListAsync();
            }
            else
            {
                return await _context.Zanr.Where(x => x.Naziv.Contains(name)).ToListAsync();
            }
        }

        // GET: api/Zanrovi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zanr>> GetZanr(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);

            if (zanr == null)
            {
                return NotFound();
            }

            return zanr;
        }

        // PUT: api/Zanrovi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZanr(int id, Zanr zanr)
        {
            if (id != zanr.Id)
            {
                return BadRequest();
            }

            if (ZanrExists(zanr.Naziv, zanr.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.zanr_err);
            }

            _context.Entry(zanr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZanrExists(id))
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

        // POST: api/Zanrovi
        [HttpPost]
        public async Task<ActionResult<Zanr>> PostZanr(Zanr zanr)
        {
            if (ZanrExists(zanr.Naziv))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.zanr_err);
            }

            _context.Zanr.Add(zanr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZanr", new { id = zanr.Id }, zanr);
        }

        // DELETE: api/Zanrovi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Zanr>> DeleteZanr(int id)
        {
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return NotFound();
            }

            try
            {
                _context.Zanr.Remove(zanr);
                await _context.SaveChangesAsync();

                return zanr;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool ZanrExists(int id)
        {
            return _context.Zanr.Any(e => e.Id == id);
        }

        private bool ZanrExists(string name, int? id = null)
        {
            if (id != null)
            {
                return _context.Zanr.Any(e => e.Naziv.ToLower().Equals(name.ToLower()) && e.Id != id.Value);
            }
            else
            {
                return _context.Zanr.Any(e => e.Naziv.ToLower().Equals(name.ToLower()));
            }
        }
    }
}
