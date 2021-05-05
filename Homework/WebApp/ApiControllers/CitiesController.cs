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
using City = Domain.App.City;


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for City
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class CitiesController : ControllerBase
    {

        private readonly CityMapper _mapper = new CityMapper();
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CitiesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.City), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.City>>> GetCities()
        {
            return Ok((await _bll.City.GetAllAsync()).Select(s => new PublicApi.DTO.v1.City()
            {
                Id = s.Id,
                Name = s.Name,
                NameId = s.NameId
            }));
        }

        /// <summary>
        /// Get one city. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>City entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.City), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.City>> GetCity(Guid id)
        {
            var city = await _bll.City.FirstOrDefaultAsync(id);

            if (city == null)
            {
                return NotFound(new Message("City not found"));
            }

            return Ok(_mapper.Map(city));
        }

        /// <summary>
        /// Update city
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutCity(Guid id, PublicApi.DTO.v1.City city)
        {
            if (id != city.Id)
            {
                return NotFound(new Message("Id and city.id do not match"));
            }

            _bll.City.Update(_mapper.Map(city));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post city
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(City))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.City>> PostCity(PublicApi.DTO.v1.City city)
        {
            _bll.City.Add(_mapper.Map(city));
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCity",
                new
                {
                    id = city.Id

                }, city);
        }

        /// <summary>
        /// Delete city
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.City))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _bll.City.FirstOrDefaultAsync(id);
            if (city == null)
            {
                return NotFound(new Message("City not found"));
            }

            _bll.City.Remove(city);
            await _bll.SaveChangesAsync();

            return Ok(city);
        }

    }
}
