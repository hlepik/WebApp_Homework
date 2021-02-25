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
    public class UserProductsController : Controller
    {
        private readonly AppDbContext _context;

        public UserProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserProducts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserProducts.Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserProducts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProducts = await _context.UserProducts
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProducts == null)
            {
                return NotFound();
            }

            return View(userProducts);
        }

        // GET: UserProducts/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(_context.Bookings, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName");
            return View();
        }

        // POST: UserProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateAdded,DateRemoved,UserId,BookingId,ProductTypeId")] UserProducts userProducts)
        {
            if (ModelState.IsValid)
            {
                userProducts.Id = Guid.NewGuid();
                _context.Add(userProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userProducts.UserId);
            return View(userProducts);
        }

        // GET: UserProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProducts = await _context.UserProducts.FindAsync(id);
            if (userProducts == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userProducts.UserId);
            return View(userProducts);
        }

        // POST: UserProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DateAdded,DateRemoved,UserId,BookingId,ProductTypeId")] UserProducts userProducts)
        {
            if (id != userProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProductsExists(userProducts.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "FirstName", userProducts.UserId);
            return View(userProducts);
        }

        // GET: UserProducts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProducts = await _context.UserProducts
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProducts == null)
            {
                return NotFound();
            }

            return View(userProducts);
        }

        // POST: UserProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userProducts = await _context.UserProducts.FindAsync(id);
            _context.UserProducts.Remove(userProducts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProductsExists(Guid id)
        {
            return _context.UserProducts.Any(e => e.Id == id);
        }
    }
}
