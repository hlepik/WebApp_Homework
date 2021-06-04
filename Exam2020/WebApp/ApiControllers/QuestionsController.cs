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

    public class QuestionsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public QuestionsController(IAppUnitOfWork uow)
        {
            _uow = uow;

        }

        // GET: api/Questions
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            var res = await _uow.Question.GetAllAsync();
            return Ok(res);
        }

        /// <summary>
        /// Get one question. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Question entity from db</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Question), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<Question>> GetQuestion(Guid id)
        {
            var question = await _uow.Question.FirstOrDefaultAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        /// <summary>
        /// Update picture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]
        public async Task<IActionResult> PutQuestion(Guid id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _uow.Question.Update(question);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post picture
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Question))]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            _uow.Question.Add(question);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        /// <summary>
        /// Delete question
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Question))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            var question = await _uow.Question.FirstOrDefaultAsync(id);

            if (question != null)
            {
                _uow.Answer.RemoveAnswerAsync(question.Id);
            }

            await _uow.Question.RemoveAsync(id);
            await _uow.SaveChangesAsync();

            return NoContent();
        }


    }
}
