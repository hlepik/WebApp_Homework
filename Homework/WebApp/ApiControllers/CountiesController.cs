using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class CountiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public CountiesController(AppDbContext context,  IAppBLL bll)
        {
            _context = context;
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
        public async Task<ActionResult<County>> GetCounty(Guid id)
        {
            var county = await _context.Counties.FindAsync(id);

            if (county == null)
            {
                return NotFound();
            }

            return county;
        }

        // PUT: api/Counties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounty(Guid id, County county)
        {
            if (id != county.Id)
            {
                return BadRequest();
            }

            _context.Entry(county).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountyExists(id))
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

        // POST: api/Counties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<County>> PostCounty(County county)
        {
            _context.Counties.Add(county);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounty", new { id = county.Id }, county);
        }

        // DELETE: api/Counties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounty(Guid id)
        {
            var county = await _context.Counties.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }

            _context.Counties.Remove(county);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountyExists(Guid id)
        {
            return _context.Counties.Any(e => e.Id == id);
        }
    }
}
