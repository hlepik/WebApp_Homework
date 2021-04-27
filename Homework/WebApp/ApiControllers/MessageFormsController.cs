using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using MessageForm = Domain.App.MessageForm;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for MessageForm
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class MessageFormsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly MessageFormMapper _mapper = new MessageFormMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public MessageFormsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all messageForms
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.MessageForm), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.MessageForm>>> GetMessageForms()
        {
            return Ok((await _bll.MessageForm.GetAllAsync()).Select(s => new PublicApi.DTO.v1.MessageForm()
            {
                Id = s.Id,
                Message = s.Message,
                Email = s.Email,
                Subject = s.Subject,
                DateSent = s.DateSent,
                SenderId = s.SenderId
            }));
        }

        /// <summary>
        /// Get one Message form. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>MessageForm entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.MessageForm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.MessageForm>> GetMessageForm(Guid id)
        {
            var messageForm = await _bll.MessageForm.FirstOrDefaultMessagesAsync(id);

            if (messageForm == null)
            {
                return NotFound(new Message("Message form not found"));
            }

            return Ok(_mapper.Map(messageForm));
        }

        /// <summary>
        /// Update messageForm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="messageForm"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]

        public async Task<IActionResult> PutMessageForm(Guid id, PublicApi.DTO.v1.MessageForm messageForm)
        {
            if (id != messageForm.Id)
            {
                return NotFound(new Message("Id and messageForm.id do not match"));
            }

            if (!await _bll.UserMessages.ExistsAsync(messageForm.Id, User.GetUserId()!.Value))
            {
                return BadRequest(new Message($"Current user does not have send messages with this id {id}"));
            }
            _bll.MessageForm.Update(_mapper.Map(messageForm));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post messageForm
        /// </summary>
        /// <param name="messageForm"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageForm))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.MessageForm>> PostMessageForm(PublicApi.DTO.v1.MessageForm messageForm)
        {
            _bll.MessageForm.Add(_mapper.Map(messageForm));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMessageForm",
                new
                {
                    id = messageForm.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"

                }, messageForm);
        }

        /// <summary>
        /// Delete messageForm
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.MessageForm))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteMessageForm(Guid id)
        {
            var messageForm = await _bll.MessageForm.FirstOrDefaultAsync(id);
            if (messageForm == null)
            {
                return NotFound(new Message("Message form not found"));
            }


            await _bll.UserMessages.GetByMessageFormId(id);

            _bll.MessageForm.RemoveMessagesAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(messageForm);
        }

    }
}
