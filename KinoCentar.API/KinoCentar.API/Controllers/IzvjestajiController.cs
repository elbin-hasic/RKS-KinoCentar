using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Description;
using KinoCentar.API.EntityModels;
using KinoCentar.API.EntityModels.Extensions;
using KinoCentar.Shared.Models.Izvjestaji;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinoCentar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IzvjestajiController : ControllerBase
    {
        private readonly KinoCentarDbContext _context;

        public IzvjestajiController(KinoCentarDbContext context)
        {
            _context = context;
        }

        // GET: api/Izvjestaji/ProdajaPoDatumu/{odDatuma}/{doDatuma}
        [HttpGet]
        [ResponseType(typeof(ProdajaIzvjestajModel))]
        [Route("ProdajaPoDatumu/{odDatuma}/{doDatuma}")]
        public async Task<ActionResult<IEnumerable<ProdajaIzvjestajModel>>> GetProdajaPoDatumu(DateTime odDatuma, DateTime doDatuma)
        {
            var data = await _context.Prodaja
                                .Include(x => x.Korisnik).AsNoTracking()
                                .Include(x => x.ArtikliStavke)
                                .Include(x => x.RezervacijeStavke)
                             .Where(x => x.Datum.Date >= odDatuma.Date && x.Datum.Date <= doDatuma.Date)
                             .Select(x => new ProdajaExtension(x, false)).ToListAsync();

            return data.Select(x => new ProdajaIzvjestajModel 
                            { 
                                BrojRacuna = x.BrojRacuna,
                                Cijena = x.UkupnaCijena,
                                Datum = x.Datum.ToString("dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                                Korisnik = x.Korisnik?.Ime
                            }).ToList();
        }

        // GET: api/Izvjestaji/RezervacijaPoDatumu/{odDatuma}/{doDatuma}/{onlyUsed}
        [HttpGet]
        [ResponseType(typeof(RezervacijaIzvjestajModel))]
        [Route("RezervacijaPoDatumu/{odDatuma}/{doDatuma}/{onlyUsed}")]
        public async Task<ActionResult<IEnumerable<RezervacijaIzvjestajModel>>> GetRezervacijaPoDatumu(DateTime odDatuma, DateTime doDatuma, bool onlyUsed)
        {
            var data = await _context.Rezervacija
                                .Include(x => x.Korisnik).AsNoTracking()
                                .Include(x => x.ProjekcijaTermin)
                                    .ThenInclude(x => x.Projekcija)
                                    .ThenInclude(x => x.Film).AsNoTracking()
                                .Include(x => x.ProjekcijaTermin)
                                    .ThenInclude(x => x.Projekcija)
                                    .ThenInclude(x => x.Sala).AsNoTracking()
                             .Where(x => x.KorisnikId != null && 
                                         x.Datum.Date >= odDatuma.Date && 
                                         x.Datum.Date <= doDatuma.Date).ToListAsync();

            if (onlyUsed)
            {
                data = data.Where(x => x.DatumProdano != null).ToList();
            }

            return data.Select(x => new RezervacijaIzvjestajModel
            {
                FilmNaslov = x.ProjekcijaTermin.Projekcija.Film.Naslov,
                SalaNaziv = x.ProjekcijaTermin.Projekcija.Sala.Naziv,
                BrojSjedista = x.BrojSjedista,
                Cijena = x.Cijena,
                TerminProjekcije = $"{x.DatumProjekcije.ToShortDateString()} {x.ProjekcijaTermin.Termin:hh\\:mm}",
                KorisnikImePrezime = $"{x.Korisnik?.Ime} {x.Korisnik?.Prezime}"
            }).ToList();
        }
    }
}