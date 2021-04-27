using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.Base;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LangStringsController : Controller
    {
        private readonly AppDbContext _context;

        public LangStringsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/LangStrings
        public async Task<IActionResult> Index()
        {
            return View(await _context.LangStrings.ToListAsync());
        }

        // GET: Admin/LangStrings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langString = await _context.LangStrings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langString == null)
            {
                return NotFound();
            }

            return View(langString);
        }

        // GET: Admin/LangStrings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LangStrings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] LangString langString)
        {
            if (ModelState.IsValid)
            {
                langString.Id = Guid.NewGuid();
                _context.Add(langString);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(langString);
        }

        // GET: Admin/LangStrings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langString = await _context.LangStrings.FindAsync(id);
            if (langString == null)
            {
                return NotFound();
            }
            return View(langString);
        }

        // POST: Admin/LangStrings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id")] LangString langString)
        {
            if (id != langString.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(langString);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LangStringExists(langString.Id))
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
            return View(langString);
        }

        // GET: Admin/LangStrings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var langString = await _context.LangStrings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (langString == null)
            {
                return NotFound();
            }

            return View(langString);
        }

        // POST: Admin/LangStrings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var langString = await _context.LangStrings.FindAsync(id);
            _context.LangStrings.Remove(langString);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LangStringExists(Guid id)
        {
            return _context.LangStrings.Any(e => e.Id == id);
        }
    }
}
