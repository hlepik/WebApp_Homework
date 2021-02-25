using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class UserBookingsController : Controller
    {
        private readonly AppDbContext _context;

        public UserBookingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserBookings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserBookings.Include(u => u.Booking).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserBookings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooking = await _context.UserBookings
                .Include(u => u.Booking)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBooking == null)
            {
                return NotFound();
            }

            return View(userBooking);
        }

        // GET: UserBookings/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName");
            return View();
        }

        // POST: UserBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateBooked,DateRemoved,BookingId,UserId")] UserBooking userBooking)
        {
            if (ModelState.IsValid)
            {
                userBooking.Id = Guid.NewGuid();
                _context.Add(userBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", userBooking.BookingId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userBooking.UserId);
            return View(userBooking);
        }

        // GET: UserBookings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooking = await _context.UserBookings.FindAsync(id);
            if (userBooking == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", userBooking.BookingId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userBooking.UserId);
            return View(userBooking);
        }

        // POST: UserBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateBooked,DateRemoved,BookingId,UserId")] UserBooking userBooking)
        {
            if (id != userBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBookingExists(userBooking.Id))
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
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", userBooking.BookingId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userBooking.UserId);
            return View(userBooking);
        }

        // GET: UserBookings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBooking = await _context.UserBookings
                .Include(u => u.Booking)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var userBooking = await _context.UserBookings.FindAsync(id);
            _context.UserBookings.Remove(userBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBookingExists(Guid id)
        {
            return _context.UserBookings.Any(e => e.Id == id);
        }
    }
}
