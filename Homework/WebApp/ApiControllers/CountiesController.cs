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


    public class CountiesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CountiesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Counties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<County>>> GetCounties()
        {
            return Ok(await _bll.County.GetAllAsync());
        }

        // GET: api/Counties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.County>> GetCounty(Guid id)
        {
            var county = await _bll.County.FirstOrDefaultAsync(id);

            if (county == null)
            {
                return NotFound();
            }

            return county;
        }

        // PUT: api/Counties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounty(Guid id, BLL.App.DTO.County county)
        {
            if (id != county.Id)
            {
                return BadRequest();
            }

            _bll.County.Update(county);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Counties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<County>> PostCounty(BLL.App.DTO.County county)
        {
            _bll.County.Add(county);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCounty", new { id = county.Id }, county);
        }

        // DELETE: api/Counties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounty(Guid id)
        {
            var county = await _bll.County.FirstOrDefaultAsync(id);
            if (county == null)
            {
                return NotFound();
            }

            _bll.County.Remove(county);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
