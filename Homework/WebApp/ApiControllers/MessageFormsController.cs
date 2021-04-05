using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MessageFormsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public MessageFormsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/MessageForms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.MessageForm>>> GetMessageForms()
        {
            return Ok(await _bll.MessageForm.GetAllAsync(User.GetUserId()!.Value));
        }

        // GET: api/MessageForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.MessageForm>> GetMessageForm(Guid id)
        {
            var messageForm = await _bll.MessageForm.FirstOrDefaultMessagesAsync(id);

            if (messageForm == null)
            {
                return NotFound();
            }

            return messageForm;
        }

        // PUT: api/MessageForms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageForm(Guid id, BLL.App.DTO.MessageForm messageForm)
        {
            if (id != messageForm.Id)
            {
                return BadRequest();
            }
            _bll.MessageForm.Update(messageForm);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/MessageForms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageForm>> PostMessageForm(BLL.App.DTO.MessageForm messageForm)
        {
            _bll.MessageForm.Add(messageForm);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMessageForm", new { id = messageForm.Id }, messageForm);
        }

        // DELETE: api/MessageForms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageForm(Guid id)
        {
            var messageForm = await _bll.MessageForm.FirstOrDefaultAsync(id);
            if (messageForm == null)
            {
                return NotFound();
            }

            _bll.MessageForm.Remove(messageForm);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
