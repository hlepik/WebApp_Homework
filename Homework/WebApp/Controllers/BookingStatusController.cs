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
    public class BookingStatusController : Controller
    {
        private readonly AppDbContext _context;

        public BookingStatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BookingStatus
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.BookingStatus.Include(b => b.Booking);
            return View(await appDbContext.ToListAsync());
        }

        // GET: BookingStatus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus
                .Include(b => b.Booking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // GET: BookingStatus/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
            return View();
        }

        // POST: BookingStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Comment,BookingId")] BookingStatus bookingStatus)
        {
            if (ModelState.IsValid)
            {
                bookingStatus.Id = Guid.NewGuid();
                _context.Add(bookingStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", bookingStatus.BookingId);
            return View(bookingStatus);
        }

        // GET: BookingStatus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus.FindAsync(id);
            if (bookingStatus == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", bookingStatus.BookingId);
            return View(bookingStatus);
        }

        // POST: BookingStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description,Comment,BookingId")] BookingStatus bookingStatus)
        {
            if (id != bookingStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingStatusExists(bookingStatus.Id))
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
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id", bookingStatus.BookingId);
            return View(bookingStatus);
        }

        // GET: BookingStatus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _context.BookingStatus
                .Include(b => b.Booking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // POST: BookingStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bookingStatus = await _context.BookingStatus.FindAsync(id);
            _context.BookingStatus.Remove(bookingStatus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingStatusExists(Guid id)
        {
            return _context.BookingStatus.Any(e => e.Id == id);
        }
    }
}
