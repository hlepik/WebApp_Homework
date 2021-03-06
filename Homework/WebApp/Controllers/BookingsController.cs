using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Extensions.Base;
using WebApp.ViewModels.Booking;
using Product = Domain.App.Product;
#pragma warning disable 1591


namespace WebApp.Controllers
{

    public class BookingsController : Controller
    {
        private readonly IAppBLL _bll;


        public BookingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Bookings

        public async Task<IActionResult> Index()
        {

            return View(await _bll.Product.GetAllProductsAsync());

        }


        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Product.FirstOrDefaultDTOAsync(id.Value);


            return View(product);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create(Guid id)
        {
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id);
            var vm = new BookingCreateEditViewModels();
            vm.Products = product!;
            return View(vm);

        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateEditViewModels vm)
        {
            var product = await _bll.Product.ChangeBookingStatus(vm.Products.Id);

            product.IsBooked = true;

            _bll.Product.Update(product);

            var booking = new Booking
            {
                TimeBooked = DateTime.Now,
                Until = vm.Booking.Until,
                AppUserId = User.GetUserId()!.Value,
                ProductId = product.Id
            };
            var myBookings = new UserBookedProducts
            {
                ProductId = product.Id,
                AppUserId = User.GetUserId()!.Value
            };
            _bll.UserBookedProducts.Add(myBookings);
            _bll.Booking.Add(booking);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bll.Booking.FirstOrDefaultDTOAsync(id.Value, User.GetUserId()!.Value);

            var vm = new BookingCreateEditViewModels();
            vm.Booking = booking;

            return View(vm);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookingCreateEditViewModels vm )
        {
            if (id != vm.Booking.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                _bll.Booking.Update(vm.Booking);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bll.Booking.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {


            await _bll.Booking.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
