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

    public class UserBookingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public UserBookingsController(AppDbContext context, IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: api/UserBookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserBookings>>> GetUserBookings()
        {
            return Ok(await _bll.UserBookings.GetAllAsync());
        }

        // GET: api/UserBookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserBookings>> GetUserBookings(Guid id)
        {
            var userBookings = await _context.UserBookings.FindAsync(id);

            if (userBookings == null)
            {
                return NotFound();
            }

            return userBookings;
        }

        // PUT: api/UserBookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserBookings(Guid id, UserBookings userBookings)
        {
            if (id != userBookings.Id)
            {
                return BadRequest();
            }

            _context.Entry(userBookings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBookingsExists(id))
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

        // POST: api/UserBookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserBookings>> PostUserBookings(UserBookings userBookings)
        {
            _context.UserBookings.Add(userBookings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserBookings", new { id = userBookings.Id }, userBookings);
        }

        // DELETE: api/UserBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserBookings(Guid id)
        {
            var userBookings = await _context.UserBookings.FindAsync(id);
            if (userBookings == null)
            {
                return NotFound();
            }

            _context.UserBookings.Remove(userBookings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserBookingsExists(Guid id)
        {
            return _context.UserBookings.Any(e => e.Id == id);
        }
    }
}
