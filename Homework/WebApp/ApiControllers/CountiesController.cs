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
    /// API controller for County
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class CountiesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CountyMapper _mapper = new CountyMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CountiesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all counties
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.County), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.County>>> GetCounties()
        {
            return Ok((await _bll.County.GetAllAsync()).Select(s => new PublicApi.DTO.v1.County()
            {
                Id = s.Id,
                Name = s.Name
            }));
        }

        /// <summary>
        /// Get one county. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>County entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.County), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.County>> GetCounty(Guid id)
        {
            var county = await _bll.County.FirstOrDefaultAsync(id);

            if (county == null)
            {
                return NotFound(new Message("County not found"));
            }

            return Ok(_mapper.Map(county));
        }

        /// <summary>
        /// Update county
        /// </summary>
        /// <param name="id"></param>
        /// <param name="county"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutCounty(Guid id, PublicApi.DTO.v1.County county)
        {
            if (id != county.Id)
            {
                return NotFound(new Message("Id and county.id do not match"));
            }


            _bll.County.Update(_mapper.Map(county));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post county
        /// </summary>
        /// <param name="county"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(County))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.County>> PostCounty(PublicApi.DTO.v1.County county)
        {
            _bll.County.Add(_mapper.Map(county));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetCounty",
                new
                {
                    id = county.Id

                }, county);
        }

        /// <summary>
        /// Delete county
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.County))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteCounty(Guid id)
        {
            var county = await _bll.County.FirstOrDefaultAsync(id);
            if (county == null)
            {
                return NotFound(new Message("County not found"));
            }

            _bll.County.Remove(county);
            await _bll.SaveChangesAsync();

            return Ok(county);
        }

    }
}
