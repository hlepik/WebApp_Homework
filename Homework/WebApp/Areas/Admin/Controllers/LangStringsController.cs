using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.Base;

#pragma warning disable 1591

namespace WebApp.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class LangStringsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        public LangStringsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all LangStrings
        /// </summary>
        /// <returns>Entities from db</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.LangStrings.ToListAsync());
        }

        /// <summary>
        /// Get one LangString. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>UserMessages entity from db</returns>
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


        /// <summary>
        /// Create new view
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Create new
        /// </summary>
        /// <param name="langString"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LangString langString)
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
        public async Task<IActionResult> Edit(Guid id,  LangString langString)
        {
            if (id != langString.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

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

    }
}
