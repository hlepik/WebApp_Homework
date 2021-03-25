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
    public class ConditionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public ConditionsController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/Conditions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Condition>>> GetConditions()
        {
            return Ok(await _uow.Condition.GetAllAsync());
        }

        // GET: api/Conditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Condition>> GetCondition(Guid id)
        {
            var condition = await _context.Conditions.FindAsync(id);

            if (condition == null)
            {
                return NotFound();
            }

            return condition;
        }

        // PUT: api/Conditions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCondition(Guid id, Condition condition)
        {
            if (id != condition.Id)
            {
                return BadRequest();
            }

            _context.Entry(condition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConditionExists(id))
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

        // POST: api/Conditions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Condition>> PostCondition(Condition condition)
        {
            _context.Conditions.Add(condition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCondition", new { id = condition.Id }, condition);
        }

        // DELETE: api/Conditions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondition(Guid id)
        {
            var condition = await _context.Conditions.FindAsync(id);
            if (condition == null)
            {
                return NotFound();
            }

            _context.Conditions.Remove(condition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConditionExists(Guid id)
        {
            return _context.Conditions.Any(e => e.Id == id);
        }
    }
}
