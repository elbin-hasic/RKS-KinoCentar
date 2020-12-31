using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KinoCentar.API.EntityModels;
using System.Net;
using KinoCentar.API.EntityModels.Extensions;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Authorization;
using KinoCentar.Shared;
using KinoCentar.Shared.Extensions;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdajaController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public ProdajaController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Prodaja
        [HttpGet]
        [ResponseType(typeof(ProdajaExtension))]
        public async Task<ActionResult<IEnumerable<ProdajaExtension>>> GetProdaja()
        {
            return await _context.Prodaja
                        .Include(x => x.Korisnik).AsNoTracking()
                        .Include(x => x.ArtikliStavke)
                        .Include(x => x.RezervacijeStavke)
                        .Select(x => new ProdajaExtension(x, false)).ToListAsync();
        }

        // GET: api/Prodaja/SearchByNumber/{number?}
        [HttpGet]
        [ResponseType(typeof(ProdajaExtension))]
        [Route("SearchByNumber/{number?}")]
        public async Task<ActionResult<IEnumerable<ProdajaExtension>>> GetProdaja(string number = "")
        {
            if (string.IsNullOrEmpty(number))
            {
                return await _context.Prodaja
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ArtikliStavke)
                            .Include(x => x.RezervacijeStavke)
                            .Select(x => new ProdajaExtension(x, false)).ToListAsync();
            }
            else
            {
                return await _context.Prodaja
                            .Include(x => x.Korisnik).AsNoTracking()
                            .Include(x => x.ArtikliStavke)
                            .Include(x => x.RezervacijeStavke)
                            .Where(x => x.BrojRacuna.Contains(number))
                            .Select(x => new ProdajaExtension(x, false)).ToListAsync();
            }
        }

        // GET: api/Prodaja/5
        [HttpGet("{id}")]
        [ResponseType(typeof(ProdajaExtension))]
        public async Task<ActionResult<Prodaja>> GetProdaja(int id)
        {
            var prodaja = await _context.Prodaja
                                .Include(x => x.Korisnik).AsNoTracking()
                                .Include(x => x.ArtikliStavke)
                                    .ThenInclude(y => y.Artikal).AsNoTracking()
                                .Include(x => x.RezervacijeStavke)
                                    .ThenInclude(y => y.Rezervacija)
                                    .ThenInclude(y => y.ProjekcijaTermin).AsNoTracking()
                                .Include(x => x.RezervacijeStavke)
                                    .ThenInclude(y => y.Rezervacija)
                                    .ThenInclude(y => y.ProjekcijaTermin)
                                    .ThenInclude(y => y.Projekcija)
                                    .ThenInclude(y => y.Film).AsNoTracking()
                                .Include(x => x.RezervacijeStavke)
                                    .ThenInclude(y => y.Rezervacija)
                                    .ThenInclude(y => y.ProjekcijaTermin)
                                    .ThenInclude(y => y.Projekcija)
                                    .ThenInclude(y => y.Sala).AsNoTracking()
                                .Include(x => x.RezervacijeStavke)
                                    .ThenInclude(y => y.Rezervacija)
                                    .ThenInclude(y => y.Korisnik).AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);
            if (prodaja == null)
            {
                return NotFound();
            }

            return new ProdajaExtension(prodaja, true);
        }

        // PUT: api/Prodaja/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdaja(int id, Prodaja prodaja)
        {
            if (id != prodaja.Id)
            {
                return BadRequest();
            }

            _context.Entry(prodaja).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdajaExists(id))
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

        // POST: api/Prodaja
        [HttpPost]
        public async Task<ActionResult<Prodaja>> PostProdaja(Prodaja prodaja)
        {
            try
            {
                prodaja.BrojRacuna = GenerateBrojRacuna();
                if (string.IsNullOrEmpty(prodaja.BrojRacuna))
                {
                    return StatusCode((int)HttpStatusCode.Conflict, Messages.racun_br_err);
                }

                if (prodaja.ArtikliStavke != null)
                {
                    foreach (var artikalStavka in prodaja.ArtikliStavke)
                    {
                        artikalStavka.Prodaja = prodaja;
                        _context.ProdajaArtikalDodjela.Add(artikalStavka);
                    }
                }                

                if (prodaja.RezervacijeStavke != null)
                {
                    foreach (var rezStavka in prodaja.RezervacijeStavke)
                    {
                        var rezervacija = await _context.Rezervacija.FindAsync(rezStavka.RezervacijaId);
                        if (rezervacija != null)
                        {
                            rezervacija.DatumProdano = DateTime.Now;
                        }

                        rezStavka.Prodaja = prodaja;
                        _context.ProdajaRezervacijaDodjela.Add(rezStavka);
                    }
                }

                _context.Prodaja.Add(prodaja);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProdaja", new { id = prodaja.Id }, prodaja);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }            
        }

        // DELETE: api/Prodaja/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prodaja>> DeleteProdaja(int id)
        {
            var prodaja = await _context.Prodaja.FindAsync(id);
            if (prodaja == null)
            {
                return NotFound();
            }

            try
            {
                var datumProvjera = prodaja.Datum.AddMinutes(10);
                if (datumProvjera < DateTime.Now)
                {
                    return StatusCode((int)HttpStatusCode.Conflict, Messages.prodaja_del_err);
                }

                _context.Prodaja.Remove(prodaja);
                await _context.SaveChangesAsync();

                return prodaja;
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.ReadLastExceptionMessage());
            }
        }

        private string GenerateBrojRacuna()
        {
            var dtn = DateTime.Now;
            string brojRacuna = string.Empty;
            int brojRacuna_br = 1;

            bool isBrojRacunaOk = false;
            int i = 0;

            var lastProdaja = _context.Prodaja.OrderByDescending(x => x.Datum)
                                              .ThenByDescending(x => x.Id).FirstOrDefault();

            do
            {
                if (lastProdaja != null)
                {
                    try
                    {
                        var splitedBrojRacuna = lastProdaja.BrojRacuna.Split('/');
                        int year = int.Parse(splitedBrojRacuna[0]);
                        int month = int.Parse(splitedBrojRacuna[1]);
                        int last_br = int.Parse(splitedBrojRacuna[2]);

                        if (dtn.Year == year && dtn.Month == month)
                        {                            
                            brojRacuna_br = last_br + 1;
                        }
                    }
                    catch
                    {}
                }

                brojRacuna = $"{dtn.Year}/{dtn.Month}/{brojRacuna_br}";

                isBrojRacunaOk = !_context.Prodaja.Any(e => e.BrojRacuna.ToLower().Equals(brojRacuna.ToLower()));
                i++;
            } while (!isBrojRacunaOk && i < 5);

            if (!isBrojRacunaOk)
            {
                brojRacuna = string.Empty;
            }

            return brojRacuna;
        }

        private bool ProdajaExists(int id)
        {
            return _context.Prodaja.Any(e => e.Id == id);
        }
    }
}
