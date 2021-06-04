using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]

    public class AnswersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public AnswersController(IAppUnitOfWork uow)
        {
            _uow = uow;

        }

        // GET: api/Answers
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var res = await _uow.Answer.GetAllAsync();
            return Ok(res);
        }

        /// <summary>
        /// Get one answer. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Picture entity from db</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Answer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<Answer>> GetAnswer(Guid id)
        {
            var answer = await _uow.Answer.FirstOrDefaultAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        /// <summary>
        /// Update picture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            _uow.Answer.Update(answer);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post picture
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Answer))]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            _uow.Answer.Add(answer);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        /// <summary>
        /// Delete answer
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Answer))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteAnswer(Guid id)
        {
            var answer = await _uow.Answer.FirstOrDefaultAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _uow.Answer.Remove(answer);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

    }
}
