using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public UnitsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return Ok(await _uow.Unit.GetAllAsync());
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(Guid id)
        {
            var unit = await _context.Units.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(Guid id, Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.Id }, unit);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(Guid id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
