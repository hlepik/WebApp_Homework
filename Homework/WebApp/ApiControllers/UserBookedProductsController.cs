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
using Product = Domain.App.Product;


namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for UserBookedProducts
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserBookedProductsController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly UserBookedProductsMapper _mapper = new UserBookedProductsMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public UserBookedProductsController(IAppBLL bll)
        {

            _bll = bll;
        }

        /// <summary>
        /// Get all userBookedProducts
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.UserBookedProducts), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.UserBookedProducts>>> GetUserBookedProducts()
        {

            return Ok((await _bll.UserBookedProducts.GetAllAsync(User.GetUserId()!.Value))
                .Select(s => new PublicApi.DTO.v1.UserBookedProducts()
                {
                    Id = s.Id,
                    Description = s.Description,
                    AppUserId = s.AppUserId,
                    Email = s.Email,
                    Until = s.Until,
                    TimeBooked = s.TimeBooked,
                    ProductId = s.ProductId,
                    BookingId = s.BookingId
                }));
        }

        /// <summary>
        /// Get one user booked product. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>userBookedProducts entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.UserBookedProducts), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.UserBookedProducts>> GetUserBookedProducts(Guid id)
        {
            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultBookedProductsAsync(id);

            if (userBookedProducts == null)
            {
                return NotFound(new Message("User booked products not found"));
            }

            return Ok(_mapper.Map(userBookedProducts));
        }

        /// <summary>
        /// Update userBookedProducts
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userBookedProducts"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]

        public async Task<IActionResult> PutUserBookedProducts(Guid id, PublicApi.DTO.v1.UserBookedProducts userBookedProducts)
        {

            if (id != userBookedProducts.Id)
            {
                return NotFound(new Message("Id and userBookedProducts.id do not match"));
            }

            _bll.UserBookedProducts.Update(_mapper.Map(userBookedProducts));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post userBookedProducts
        /// </summary>
        /// <param name="userBookedProducts"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserBookedProducts))]
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.v1.UserBookedProducts>> PostUserBookedProducts(PublicApi.DTO.v1.UserBookedProducts userBookedProducts)
        {
            _bll.UserBookedProducts.Add(_mapper.Map(userBookedProducts));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetUserBookedProducts",
                new
                {
                    id = userBookedProducts.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"

                }, userBookedProducts);
        }

        /// <summary>
        /// Delete userBookedProducts
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.UserBookedProducts))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]
        public async Task<IActionResult> DeleteUserBookedProducts(Guid id)
        {
            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultBookedProductsAsync(id, User.GetUserId()!.Value);
            if (userBookedProducts == null)
            {
                return NotFound(new Message("User user booked product not found"));
            }

            _bll.UserBookedProducts.Remove(userBookedProducts);
            await _bll.SaveChangesAsync();

            return Ok(userBookedProducts);
        }

    }
}
