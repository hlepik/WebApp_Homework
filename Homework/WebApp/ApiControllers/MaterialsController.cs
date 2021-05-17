using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Material
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class MaterialsController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly MaterialMapper _mapper = new MaterialMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public MaterialsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all materials
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Material), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Material>>> GetMaterials()
        {
            return Ok((await _bll.Material.GetAllAsync()).Select(s => new PublicApi.DTO.v1.Material()
            {
                Id = s.Id,
                Name = s.Name,
                Comment = s.Comment
            }));
        }

        /// <summary>
        /// Get one material. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Material entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Material), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.Material>> GetMaterial(Guid id)
        {
            var material = await _bll.Material.FirstOrDefaultAsync(id);

            if (material == null)
            {
                return NotFound(new Message("Material not found"));
            }

            return Ok(_mapper.Map(material));
        }

        /// <summary>
        /// Update material
        /// </summary>
        /// <param name="id"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutMaterial(Guid id, PublicApi.DTO.v1.Material material)
        {
            if (id != material.Id)
            {
                return NotFound(new Message("Id and material.id do not match"));
            }


            _bll.Material.Update(_mapper.Map(material));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post material
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Material))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.Material>> PostMaterial(PublicApi.DTO.v1.Material material)
        {
            _bll.Material.Add(_mapper.Map(material));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetMaterial",
                new
                {
                    id = material.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"

                }, material);
        }

        /// <summary>
        /// Delete material
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Material))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            var material = await _bll.Material.FirstOrDefaultAsync(id);
            if (material == null)
            {
                 return NotFound(new Message("Material not found"));
            }

            _bll.Material.Remove(material);
            await _bll.SaveChangesAsync();

            return Ok(material);
        }

    }
}
