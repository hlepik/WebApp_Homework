using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class UserMessagesController : ControllerBase
    {

        private readonly IAppBLL _bll;

        public UserMessagesController(IAppBLL bll)
        {
            _bll = bll;

        }

        // GET: api/UserMessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.UserMessages>>> GetUserMessages()
        {
            return Ok(await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(User.GetUserId()!.Value));
        }

        // GET: api/UserMessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.UserMessages>> GetUserMessages(Guid id)
        {
            var userMessages = await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id);

            if (userMessages == null)
            {
                return NotFound();
            }

            return userMessages;
        }

        // PUT: api/UserMessages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserMessages(Guid id, BLL.App.DTO.UserMessages userMessages)
        {
            if (id != userMessages.Id)
            {
                return BadRequest();
            }

            _bll.UserMessages.Update(userMessages);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/UserMessages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserMessages>> PostUserMessages(BLL.App.DTO.UserMessages userMessages)
        {
            _bll.UserMessages.Add(userMessages);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserMessages", new { id = userMessages.Id }, userMessages);
        }

        // DELETE: api/UserMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserMessages(Guid id)
        {
            var userMessages = await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id);
            if (userMessages == null)
            {
                return NotFound();
            }

            _bll.UserMessages.Remove(userMessages);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
