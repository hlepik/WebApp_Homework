using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UnitsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UnitsController(IAppBLL bll)
        {

            _bll = bll;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return Ok(await _bll.Unit.GetAllAsync());
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Unit>> GetUnit(Guid id)
        {
            var unit = await _bll.Unit.FirstOrDefaultAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(Guid id, BLL.App.DTO.Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _bll.Unit.Update(unit);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Units
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(BLL.App.DTO.Unit unit)
        {
            _bll.Unit.Add(unit);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.Id }, unit);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            var unit = await _bll.Unit.FirstOrDefaultAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _bll.Unit.Remove(unit);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
