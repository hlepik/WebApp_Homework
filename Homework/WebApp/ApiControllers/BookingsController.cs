using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookingsController : ControllerBase
    {

        private readonly IAppBLL _bll;


        public BookingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BLL.App.DTO.Booking>>> GetBookings()
        {

            return Ok(await _bll.Product.GetAllProductsAsync());

        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BLL.App.DTO.Booking>> GetBooking(Guid id)
        {
            var booking = await _bll.Booking.FirstOrDefaultDTOAsync(id);

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(Guid id, BLL.App.DTO.Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            _bll.Booking.Update(booking);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DAL.App.DTO.Booking>> PostBooking(BLL.App.DTO.Booking booking)
        {
            _bll.Booking.Add(booking);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            var booking = await _bll.Booking.FirstOrDefaultAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _bll.Booking.Remove(booking);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

    }
}
