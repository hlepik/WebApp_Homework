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

    public class ConditionsController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public ConditionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Conditions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Condition>>> GetConditions()
        {
            return Ok(await _bll.Condition.GetAllAsync());
        }

        // GET: api/Conditions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Condition>> GetCondition(Guid id)
        {
            var condition = await _bll.Condition.FirstOrDefaultAsync(id);

            if (condition == null)
            {
                return NotFound();
            }

            return condition;
        }

        // PUT: api/Conditions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCondition(Guid id, BLL.App.DTO.Condition condition)
        {
            if (id != condition.Id)
            {
                return BadRequest();
            }

            _bll.Condition.Update(condition);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Conditions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Condition>> PostCondition(BLL.App.DTO.Condition condition)
        {
            _bll.Condition.Add(condition);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCondition", new { id = condition.Id }, condition);
        }

        // DELETE: api/Conditions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondition(Guid id)
        {
            var condition = await _bll.Condition.FirstOrDefaultAsync(id);
            if (condition == null)
            {
                return NotFound();
            }

            _bll.Condition.Remove(condition);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
