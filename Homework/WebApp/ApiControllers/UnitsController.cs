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


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Unit
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]


    public class UnitsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UnitMapper _mapper = new UnitMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public UnitsController(IAppBLL bll)
        {

            _bll = bll;
        }

        /// <summary>
        /// Get all units
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Unit), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Unit>>> GetUnits()
        {
            return Ok((await _bll.Unit.GetAllAsync()).Select(a => _mapper.Map(a)));

        }

        /// <summary>
        /// Get one unit. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Unit entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [AllowAnonymous]
        public async Task<ActionResult<PublicApi.DTO.v1.Unit>> GetUnit(Guid id)
        {
            var unit = await _bll.Unit.FirstOrDefaultAsync(id);

            if (unit == null)
            {
                return NotFound(new Message("Unit not found"));
            }

            return Ok(_mapper.Map(unit));
        }

        /// <summary>
        /// Update unit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutUnit(Guid id, PublicApi.DTO.v1.Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound(new Message("Id and unit.id do not match"));
            }


            _bll.Unit.Update(_mapper.Map(unit));
            await _bll.SaveChangesAsync();
            return NoContent();
        }


        /// <summary>
        /// Post unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Unit))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Unit>> PostUnit(PublicApi.DTO.v1.Unit unit)
        {
            _bll.Unit.Add(_mapper.Map(unit));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUnit",
                new
                {
                    id = unit.Id

                }, unit);
        }

        /// <summary>
        /// Delete unit
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Unit))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteUnit(Guid id)
        {
            var unit = await _bll.Unit.FirstOrDefaultAsync(id);
            if (unit == null)
            {
                return NotFound(new Message("Unit not found"));
            }

            _bll.Unit.Remove(unit);
            await _bll.SaveChangesAsync();

            return Ok(unit);
        }

    }
}
