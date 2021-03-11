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
    public class BookingStatusController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public BookingStatusController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: BookingStatus
        public async Task<IActionResult> Index()
        {
            var res = await _uow.BookingStatus.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);

        }

        // GET: BookingStatus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _uow.BookingStatus
                .FirstOrDefaultAsync(id.Value);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return View(bookingStatus);
        }

        // GET: BookingStatus/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BookingId"] = new SelectList(await _uow.BookingStatus.GetAllAsync(), "Id", "Id");
            return View();
        }

        // POST: BookingStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingStatus bookingStatus)
        {
            if (ModelState.IsValid)
            {
                _uow.BookingStatus.Add(bookingStatus);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(false), "Id", "Id", bookingStatus.BookingId);
            return View(bookingStatus);
        }

        // GET: BookingStatus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _uow.BookingStatus.FirstOrDefaultAsync(id.Value);
            if (bookingStatus == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(), "Id", "Id", bookingStatus.BookingId);
            return View(bookingStatus);
        }

        // POST: BookingStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookingStatus bookingStatus)
        {
            if (id != bookingStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.BookingStatus.Update(bookingStatus);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BookingStatusExists(bookingStatus.Id))
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
            ViewData["BookingId"] = new SelectList(await _uow.Booking.GetAllAsync(), "Id", "Id", bookingStatus.BookingId);
            return View(bookingStatus);
        }

        // GET: BookingStatus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingStatus = await _uow.BookingStatus.FirstOrDefaultAsync(id.Value);
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
            await _uow.BookingStatus.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookingStatusExists(Guid id)
        {
            return await _uow.BookingStatus.ExistsAsync(id);
        }

    }
}
