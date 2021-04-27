using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using Condition = Domain.App.Condition;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Condition
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ConditionsController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly ConditionMapper _mapper = new ConditionMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ConditionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all conditions
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Condition), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Condition>>> GetConditions()
        {
            return Ok((await _bll.Condition.GetAllAsync()).Select(s => new PublicApi.DTO.v1.Condition()
            {
                Id = s.Id,
                Description = s.Description
            }));
        }

        /// <summary>
        /// Get one condition. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Condition entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Condition), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.Condition>> GetCondition(Guid id)
        {
            var condition = await _bll.Condition.FirstOrDefaultAsync(id);

            if (condition == null)
            {
                return NotFound(new Message("City not found"));
            }

            return Ok(_mapper.Map(condition));
        }

        /// <summary>
        /// Update condition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutCondition(Guid id, PublicApi.DTO.v1.Condition condition)
        {
            if (id != condition.Id)
            {
                return NotFound(new Message("Id and condition.id do not match"));
            }

            _bll.Condition.Update(_mapper.Map(condition));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Post condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Condition))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Condition>> PostCondition(PublicApi.DTO.v1.Condition condition)
        {
            _bll.Condition.Add(_mapper.Map(condition));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCondition",
                new
                {
                    id = condition.Id

                }, condition);
        }

        /// <summary>
        /// Delete condition
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Condition))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteCondition(Guid id)
        {
            var condition = await _bll.Condition.FirstOrDefaultAsync(id);
            if (condition == null)
            {
                return NotFound(new Message("Condition not found"));
            }

            _bll.Condition.Remove(condition);
            await _bll.SaveChangesAsync();

            return Ok(condition);
        }

    }
}
