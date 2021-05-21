using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for ProductMaterials
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductMaterialsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductMaterialMapper _mapper = new ProductMaterialMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductMaterialsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get all productMaterials
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ProductMaterial), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.ProductMaterial>>> GetProductMaterials()
        {

            return Ok((await _bll.ProductMaterial.GetAllProductMaterialsAsync(User.GetUserId()!.Value)).Select(a => _mapper.Map(a)));

        }

        /// <summary>
        /// Get one product material. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>ProductMaterials entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.ProductMaterial), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]

        public async Task<ActionResult<PublicApi.DTO.v1.ProductMaterial>> GetProductMaterial(Guid id)
        {
            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultDTOAsync(id, User.GetUserId()!.Value);
            if (productMaterial == null)
            {
                return NotFound(new Message("Product materials not found"));
            }
            if (User.GetUserId()!.Value == Guid.Empty)
            {
                return BadRequest(new Message("User not found"));
            }
            return Ok(productMaterial);
        }

        /// <summary>
        /// Update productMaterial
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productMaterial"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutProductMaterial(Guid id, PublicApi.DTO.v1.ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return NotFound(new Message("Id and productMaterial.id do not match"));
            }


            _bll.ProductMaterial.Update(_mapper.Map(productMaterial));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post productMaterial
        /// </summary>
        /// <param name="productMaterial"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductMaterial))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.ProductMaterial>> PostProductMaterial(PublicApi.DTO.v1.ProductMaterial productMaterial)
        {

            _bll.ProductMaterial.Add(_mapper.Map(productMaterial));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductMaterial",
                new
                {
                    id = productMaterial.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"

                }, productMaterial);
        }

        /// <summary>
        /// Delete productMaterial
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.ProductMaterial))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]

        public async Task<IActionResult> DeleteProductMaterial(Guid id)
        {
            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultDTOAsync(id, User.GetUserId()!.Value);

            if (productMaterial == null)
            {
                return NotFound(new Message("Product material not found"));
            }
            if (User.GetUserId()!.Value == Guid.Empty)
            {
                return BadRequest(new Message("User not found"));
            }

            _bll.ProductMaterial.Remove(productMaterial);
            await _bll.SaveChangesAsync();

            return Ok(productMaterial);
        }

    }
}
