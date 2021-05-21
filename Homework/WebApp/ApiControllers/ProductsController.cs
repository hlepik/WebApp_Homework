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
    /// API controller for Product
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductMapper _mapper = new ProductMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }


        /// <summary>
        /// Return last 4 inserted
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet ("four/lastFour")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Product>>> GetLastProducts()
        {

            return Ok((await _bll.Product.GetLastInserted()).Select(a => _mapper.Map(a)));


        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Product>>> GetProducts()
        {

            return Ok((await _bll.Product.GetAllProductsAsync(User.GetUserId()!.Value)).Select(a => _mapper.Map(a)));

        }


        /// <summary>
        /// Get one product. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Product entity from db</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.Product>> GetProduct(Guid? id)
        {
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id!.Value);
            if (product == null)
            {
                return NotFound(new Message("Product not found"));
            }


            return Ok(product);

        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]

        public async Task<IActionResult> PutProduct(Guid id, PublicApi.DTO.v1.Product product)
        {
            if (id !=product.Id)
            {
                return NotFound(new Message("Id and product.id do not match"));
            }

            if (!await _bll.Product.ExistsAsync(product.Id, User.GetUserId()!.Value))
            {
                return BadRequest(new Message($"Current user does not have product with this id {id}"));
            }

            _bll.Product.Update(_mapper.Map(product));
            await _bll.SaveChangesAsync();
            return NoContent();

        }

        /// <summary>
        /// Post product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        public async Task<ActionResult<Product>> PostProduct(PublicApi.DTO.v1.Product product)
        {

            product.AppUserId = User.GetUserId()!.Value;
            product.DateAdded = DateTime.Now.Date;


            _bll.Product.Add(_mapper.Map(product));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProduct",
                new
                {
                    id = product.Id,
                }, product);

        }


        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id);
            if (product == null)
            {
                return NotFound(new Message("Product not found"));
            }
            if (User.GetUserId()!.Value == Guid.Empty)
            {
                return NotFound(new Message("User not found"));
            }

            _bll.ProductMaterial.RemoveProductMaterialsAsync(id);
            _bll.UserBookedProducts.RemoveUserBookedProductsAsync(id);
            _bll.Booking.RemoveBookingAsync(id);
            _bll.Picture.RemovePictureAsync(id);
            _bll.Product.RemoveProductAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(product);

        }
    }
}
