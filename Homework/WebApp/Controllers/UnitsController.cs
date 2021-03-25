using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UnitsController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public UnitsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Units
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Unit.GetAllAsync();
            return View(res);
        }

        [AllowAnonymous]
        // GET: Units/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var units = await _uow.Unit.FirstOrDefaultAsync(id.Value);
            if (units == null)
            {
                return NotFound();
            }

            return View(units);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unit units)
        {
            if (!ModelState.IsValid) return View(units);

            _uow.Unit.Add(units);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var units = await _uow.Unit.FirstOrDefaultAsync(id.Value);
            if (units == null)
            {
                return NotFound();
            }
            return View(units);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Unit units)
        {
            if (id != units.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return View(units);

            _uow.Unit.Update(units);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var units = await _uow.Unit.FirstOrDefaultAsync(id.Value);
            if (units == null)
            {
                return NotFound();
            }

            return View(units);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Unit.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
