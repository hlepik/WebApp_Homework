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
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class ResultsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ResultsController(IAppUnitOfWork uow)
        {
            _uow = uow;

        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Quiz), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            var res = await _uow.Result.GetAllAsync(User.GetUserId()!.Value);
            return Ok(res);
        }

        /// <summary>
        /// Get one result. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Picture entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<Result>> GetResult(Guid id)
        {
            var result = await _uow.Result.FirstOrDefaultAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        /// <summary>
        /// Update picture
        /// </summary>
        /// <param name="id"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]
        public async Task<IActionResult> PutResult(Guid id, Result result)
        {
            if (id != result.Id)
            {
                return BadRequest();
            }

            _uow.Result.Update(result);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post picture
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result))]
        public async Task<ActionResult<Result>> PostResult(Result result)
        {
            result.AppUserId = User.GetUserId()!.Value;
            _uow.Result.Add(result);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetResult", new { id = result.Id }, result);
        }

        /// <summary>
        /// Delete result
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteResult(Guid id)
        {
            var result = await _uow.Result.FirstOrDefaultAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            _uow.Result.Remove(result);
            await _uow.SaveChangesAsync();

            return NoContent();
        }


    }
}
