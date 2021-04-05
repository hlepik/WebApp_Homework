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


    public class CitiesController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public CitiesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return Ok(await _bll.City.GetAllAsync());
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.City>> GetCity(Guid id)
        {
            var city = await _bll.City.FirstOrDefaultAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(Guid id, BLL.App.DTO.City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _bll.City.Update(city);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(BLL.App.DTO.City city)
        {
            _bll.City.Add(city);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _bll.City.FirstOrDefaultAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _bll.City.Remove(city);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
