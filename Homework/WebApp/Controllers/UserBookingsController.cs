using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class UserBookingsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserBookingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserBookings
        public async Task<IActionResult> Index()
        {
            var res = await _uow.UserBooking.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: UserBookings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooking = await _uow.UserBooking.FirstOrDefaultAsync(id.Value);
            if (userBooking == null)
            {
                return NotFound();
            }

            return View(userBooking);
        }

        // GET: UserBookings/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(false), "Id", "Id");
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(false), "Id", "FirstName");
            return View();
        }

        // POST: UserBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserBooking userBooking)
        {
            if (ModelState.IsValid)
            {
                _uow.UserBooking.Add(userBooking);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(), "Id", "Id", userBooking.BookingId);
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userBooking.UserId);
            return View(userBooking);
        }

        // GET: UserBookings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooking = await _uow.UserBooking.FirstOrDefaultAsync(id.Value);
            if (userBooking == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(), "Id", "Id", userBooking.BookingId);
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userBooking.UserId);
            return View(userBooking);
        }

        // POST: UserBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserBooking userBooking)
        {
            if (id != userBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.UserBooking.Update(userBooking);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserBookingExists(userBooking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(), "Id", "Id", userBooking.BookingId);
            ViewData["UserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "FirstName", userBooking.UserId);
            return View(userBooking);
        }

        // GET: UserBookings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooking = await _uow.UserBooking.FirstOrDefaultAsync(id.Value);
            if (userBooking == null)
            {
                return NotFound();
            }

            return View(userBooking);
        }

        // POST: UserBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.UserBooking.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserBookingExists(Guid id)
        {
            return await _uow.UserBooking.ExistsAsync(id);
        }
    }
}
