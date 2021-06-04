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

    public class QuizzesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public QuizzesController(IAppUnitOfWork uow)
        {
            _uow = uow;

        }

        // GET: api/Quizzes
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var res = await _uow.Quiz.GetAllAsync();
            return Ok(res);
        }

        /// <summary>
        /// Get one quiz. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Picture entity from db</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<DAL.App.DTO.Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _uow.Quiz.FirstOrDefaultAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }
        /// <summary>
        /// Get one quiz. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Quiz entity from db</returns>
        [AllowAnonymous]
        [HttpGet("correctAnswers/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public Task<int> GetCorrectAnswers(Guid id)
        {
            var quiz =  _uow.Quiz.GetCorrectAnswers(id);

            return quiz;
        }
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Entities from db</returns>
        [AllowAnonymous]
        [HttpGet("search/result/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes(string id)
        {
            var res = await _uow.Quiz.GetSearchResult(id);
            return Ok(res);


        }

        /// <summary>
        /// Update picture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]
        public async Task<IActionResult> PutQuiz(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _uow.Quiz.Update(quiz);
            await _uow.SaveChangesAsync();

            return NoContent();

        }

        /// <summary>
        /// Post picture
        /// </summary>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Quiz))]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            quiz.Percentage = 100;
            quiz.CreatedAt = DateTime.Now.ToShortDateString();
            _uow.Quiz.Add(quiz);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        /// <summary>
        /// Delete quiz
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Quiz))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var question = await _uow.Question.GetAllWithIdAsync(id);

            foreach (var each in question)
            {
                _uow.Answer.RemoveAnswerAsync(each.Id);
                await _uow.Question.RemoveAsync(each.Id);
            }
            _uow.Result.RemoveResultAsync(id);
            await _uow.Quiz.RemoveAsync(id);

            await _uow.SaveChangesAsync();

            return NoContent();

        }

    }
}
