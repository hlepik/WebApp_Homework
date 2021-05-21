using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;



namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Booking
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class BookingsController : ControllerBase
    {

        private readonly IAppBLL _bll;
        private readonly BookingMapper _mapper = new BookingMapper();
        private readonly ProductMapper _mapperProduct = new ProductMapper();
        private readonly UserBookedProductsMapper _mapperBookedProducts = new UserBookedProductsMapper();


        /// <summary>
        ///
        /// </summary>
        /// <param name="bll"></param>
        public BookingsController(IAppBLL bll)
        {
            _bll = bll;

        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Entities from db</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.v1.Product>>> GetBookings()
        {

            return Ok((await _bll.Product.GetAllProductsAsync()).Select(a => _mapperProduct.Map(a)));

        }
        /// <summary>
        /// Get one Product. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Product entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PublicApi.DTO.v1.Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<ActionResult<PublicApi.DTO.v1.Product>> GetBooking(Guid id)
        {
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id);

            if (product == null)
            {
                return NotFound(new Message("Product not found"));
            }

            return Ok(_mapperProduct.Map(product));
        }
        /// <summary>
        /// Update booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> PutBooking(Guid id, PublicApi.DTO.v1.Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound(new Message("Id and booking.id do not match"));
            }
            var product = await _bll.Product.ChangeBookingStatus(booking.ProductId);


            product.IsBooked = true;
            _bll.Product.Update(product);

            var myBookings = new UserBookedProducts
            {
                BookingId = booking.Id
            };
            _bll.UserBookedProducts.Add(_mapperBookedProducts.Map(myBookings));

            _bll.Booking.Update(_mapper.Map(booking));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Post Booking
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Booking))]

        public async Task<ActionResult<PublicApi.DTO.v1.Booking>> PostBooking(PublicApi.DTO.v1.Booking booking)
        {

            var product = await _bll.Product.ChangeBookingStatus(booking.ProductId);
            product.IsBooked = true;
            booking.TimeBooked = DateTime.Now.Date;
            _bll.Product.Update(product);
            _bll.Booking.Add(_mapper.Map(booking));
            await _bll.SaveChangesAsync();

            var myBookings = new PublicApi.DTO.v1.UserBookedProducts
            {
                AppUserId = booking.AppUserId,
                ProductId = booking.ProductId

            };
            _bll.UserBookedProducts.Add(_mapperBookedProducts.Map(myBookings));
            await _bll.SaveChangesAsync();


            return CreatedAtAction(
                "GetBooking",
                new
                {
                    id = booking.Id

                }, booking);
        }

        /// <summary>
        /// Delete booking
        /// </summary>
        /// <param name="id">Guid id of item to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublicApi.DTO.v1.Booking))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var booking = await _bll.Booking.FirstOrDefaultAsync(id);
            if (booking == null)
            {
                return NotFound(new Message($"Booking with id {id} not found"));
            }

            _bll.Booking.Remove(booking);
            await _bll.SaveChangesAsync();

            return Ok(booking);
        }

    }
}
