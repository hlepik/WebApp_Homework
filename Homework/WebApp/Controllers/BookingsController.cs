using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Booking;

namespace WebApp.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly IAppUnitOfWork _uow;


        public BookingsController(IAppUnitOfWork uow)
        {

            _uow = uow;
        }

        // GET: Bookings
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Product.GetAllProductsAsync();
            return View(res);
        }

        [AllowAnonymous]
        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultWithoutOutIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {

            var vm = new BookingCreateEditViewModels();
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description));
            return View(vm);

        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateEditViewModels vm)
        {
            if (ModelState.IsValid)
            {
                vm.Booking.UserBookingId = User.GetUserId()!.Value;



                var product = await _uow.Product.ChangeBookingStatus(vm.Booking.ProductId);
                product.IsBooked = true;

                var userBookings = new UserBookedProducts
                {
                    ProductId = vm.Booking.ProductId,
                    AppUserId = vm.Booking.UserBookingId
                };

                _uow.UserBookedProducts.Add(userBookings);

                _uow.Booking.Add(vm.Booking);
                _uow.Product.Update(product);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.Booking.ProductId);

            return View(vm);
        }


        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _uow.Booking.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (booking == null)
            {
                return NotFound();
            }
            var vm = new BookingCreateEditViewModels();
            vm.Booking = booking;
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.Booking.ProductId);
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
                _uow.Booking.Update(vm.Booking);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.Booking.ProductId);
            return View(vm);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _uow.Booking.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

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
            await _uow.Booking.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
