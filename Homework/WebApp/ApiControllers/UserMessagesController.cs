using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App.Identity;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUnitOfWork _uow;

        public UserMessagesController(AppDbContext context, IAppUnitOfWork uow)
        {
            _context = context;
            _uow = uow;
        }

        // GET: api/UserMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserMessages>>> GetUserMessages()
        {
            return Ok(await _uow.UserMessages.GetAllAsync());
        }

        // GET: api/UserMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserMessages>> GetUserMessages(Guid id)
        {
            var userMessages = await _context.UserMessages.FindAsync(id);

            if (userMessages == null)
            {
                return NotFound();
            }

            return userMessages;
        }

        // PUT: api/UserMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserMessages(Guid id, UserMessages userMessages)
        {
            if (id != userMessages.Id)
            {
                return BadRequest();
            }

            _context.Entry(userMessages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserMessagesExists(id))
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

        // POST: api/UserMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserMessages>> PostUserMessages(UserMessages userMessages)
        {
            _context.UserMessages.Add(userMessages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserMessages", new { id = userMessages.Id }, userMessages);
        }

        // DELETE: api/UserMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserMessages(Guid id)
        {
            var userMessages = await _context.UserMessages.FindAsync(id);
            if (userMessages == null)
            {
                return NotFound();
            }

            _context.UserMessages.Remove(userMessages);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserMessagesExists(Guid id)
        {
            return _context.UserMessages.Any(e => e.Id == id);
        }
    }
}
