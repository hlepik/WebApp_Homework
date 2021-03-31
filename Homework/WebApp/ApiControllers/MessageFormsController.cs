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


    public class MessageFormsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppBLL _bll;

        public MessageFormsController(AppDbContext context,  IAppBLL bll)
        {
            _context = context;
            _bll = bll;
        }

        // GET: api/MessageForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageForm>>> GetMessageForms()
        {
            return Ok(await _bll.MessageForm.GetAllAsync());
        }

        // GET: api/MessageForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageForm>> GetMessageForm(Guid id)
        {
            var messageForm = await _context.MessageForms.FindAsync(id);

            if (messageForm == null)
            {
                return NotFound();
            }

            return messageForm;
        }

        // PUT: api/MessageForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageForm(Guid id, MessageForm messageForm)
        {
            if (id != messageForm.Id)
            {
                return BadRequest();
            }

            _context.Entry(messageForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageFormExists(id))
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

        // POST: api/MessageForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageForm>> PostMessageForm(MessageForm messageForm)
        {
            _context.MessageForms.Add(messageForm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessageForm", new { id = messageForm.Id }, messageForm);
        }

        // DELETE: api/MessageForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageForm(Guid id)
        {
            var messageForm = await _context.MessageForms.FindAsync(id);
            if (messageForm == null)
            {
                return NotFound();
            }

            _context.MessageForms.Remove(messageForm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageFormExists(Guid id)
        {
            return _context.MessageForms.Any(e => e.Id == id);
        }
    }
}
