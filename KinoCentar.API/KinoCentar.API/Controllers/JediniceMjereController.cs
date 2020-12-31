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
    public class JediniceMjereController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public JediniceMjereController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/JediniceMjere
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JedinicaMjere>>> GetJedinicaMjere()
        {
            return await _context.JedinicaMjere.ToListAsync();
        }

        // GET: api/JediniceMjere/SearchByName/{name?}
        [HttpGet]
        [Route("SearchByName/{name?}")]
        public async Task<ActionResult<IEnumerable<JedinicaMjere>>> GetJedinicaMjere(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return await _context.JedinicaMjere.ToListAsync();
            }
            else
            {
                return await _context.JedinicaMjere.Where(x => x.Naziv.Contains(name)).ToListAsync();
            }
        }

        // GET: api/JediniceMjere/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JedinicaMjere>> GetJedinicaMjere(int id)
        {
            var jedinicaMjere = await _context.JedinicaMjere.FindAsync(id);

            if (jedinicaMjere == null)
            {
                return NotFound();
            }

            return jedinicaMjere;
        }

        // PUT: api/JediniceMjere/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJedinicaMjere(int id, JedinicaMjere jedinicaMjere)
        {
            if (id != jedinicaMjere.Id)
            {
                return BadRequest();
            }

            if (JedinicaMjereExists(jedinicaMjere.KratkiNaziv, jedinicaMjere.Naziv, jedinicaMjere.Id))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.jedinicaMjere_err);
            }

            _context.Entry(jedinicaMjere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JedinicaMjereExists(id))
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

        // POST: api/JediniceMjere
        [HttpPost]
        public async Task<ActionResult<JedinicaMjere>> PostJedinicaMjere(JedinicaMjere jedinicaMjere)
        {
            if (JedinicaMjereExists(jedinicaMjere.KratkiNaziv, jedinicaMjere.Naziv))
            {
                return StatusCode((int)HttpStatusCode.Conflict, Messages.jedinicaMjere_err);
            }

            _context.JedinicaMjere.Add(jedinicaMjere);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJedinicaMjere", new { id = jedinicaMjere.Id }, jedinicaMjere);
        }

        // DELETE: api/JediniceMjere/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<JedinicaMjere>> DeleteJedinicaMjere(int id)
        {
            var jedinicaMjere = await _context.JedinicaMjere.FindAsync(id);
            if (jedinicaMjere == null)
            {
                return NotFound();
            }

            try
            {
                _context.JedinicaMjere.Remove(jedinicaMjere);
                await _context.SaveChangesAsync();

                return jedinicaMjere;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private bool JedinicaMjereExists(int id)
        {
            return _context.JedinicaMjere.Any(e => e.Id == id);
        }

        private bool JedinicaMjereExists(string shortName, string name, int? id = null)
        {
            if (id != null)
            {
                return _context.JedinicaMjere.Any(e => (e.KratkiNaziv.ToLower().Equals(shortName.ToLower()) ||
                                                       e.Naziv.ToLower().Equals(name.ToLower())) && 
                                                       e.Id != id.Value);
            }
            else
            {
                return _context.JedinicaMjere.Any(e => e.KratkiNaziv.ToLower().Equals(shortName.ToLower()) || 
                                                       e.Naziv.ToLower().Equals(name.ToLower()));
            }
        }
    }
}
