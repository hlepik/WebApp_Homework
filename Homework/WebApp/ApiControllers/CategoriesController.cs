using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Category
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _mapper = new CategoryMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Category), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Category>>> GetCategories()
        {
            return Ok((await _bll.Category.GetAllAsync()).Select(a => _mapper.Map(a)));

        }

        /// <summary>
        /// Get one category. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Category entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Category), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.Category>> GetCategory(Guid id)
        {
            var category = await _bll.Category.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound(new Message("Category not found"));
            }

            return Ok(_mapper.Map(category));
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutCategory(Guid id, PublicApi.DTO.v1.Category category)
        {
            if (id != category.Id)
            {
                return NotFound(new Message("Id and category.id do not match"));
            }

            _bll.Category.Update(_mapper.Map(category));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Category>> PostCategory(PublicApi.DTO.v1.Category category)
        {

            _bll.Category.Add(_mapper.Map(category));
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetCategory",
                new
                {
                    id = category.Id

                }, category);

        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _bll.Category.FirstOrDefaultAsync(id);
            if (category == null)
            {
                return NotFound(new Message("Category not found"));
            }
            _bll.Category.Remove(category);
            await _bll.SaveChangesAsync();

            return Ok(category);
        }

    }
}
