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


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for UserMessages
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class UserMessagesController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly UserMessagesMapper _mapper = new UserMessagesMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        ///
        public UserMessagesController(IAppBLL bll)
        {
            _bll = bll;

        }

        /// <summary>
        /// Get all userMessages
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.UserMessages), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.UserMessages>>> GetUserMessages()
        {

            return Ok((await _bll.UserMessages.GetAllMessagesAsync(User.GetUserId()!.Value)).Select(a => _mapper.Map(a)));

        }

        /// <summary>
        /// Get one user message. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>UserMessages entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.UserMessages), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.UserMessages>> GetUserMessages(Guid id)
        {
            var userMessages = await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id);

            if (userMessages == null)
            {
                return NotFound(new Message("User messages not found"));
            }

            return Ok(_mapper.Map(userMessages));
        }

        /// <summary>
        /// Update userMessages
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userMessages"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> PutUserMessages(Guid id, PublicApi.DTO.v1.UserMessages userMessages)
        {
            if (id != userMessages.Id)
            {
                return NotFound(new Message("Id and userMessages.id do not match"));
            }


            _bll.UserMessages.Update(_mapper.Map(userMessages));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post userMessages
        /// </summary>
        /// <param name="userMessages"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserMessages))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.UserMessages>> PostUserMessages(PublicApi.DTO.v1.UserMessages userMessages)
        {
            _bll.UserMessages.Add(_mapper.Map(userMessages));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserMessages",
                new
                {
                    id = userMessages.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"

                }, userMessages);
        }

        /// <summary>
        /// Delete userMessages
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.UserMessages))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteUserMessages(Guid id)
        {
            var userMessages = await _bll.UserMessages.FirstOrDefaultUserMessagesAsync(id);
            if (userMessages == null)
            {
                return NotFound(new Message("User message not found"));
            }

            _bll.UserMessages.RemoveUserMessagesAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();

            return Ok(userMessages);
        }

    }
}
