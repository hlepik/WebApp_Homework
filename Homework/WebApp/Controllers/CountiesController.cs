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
    public class CountiesController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public CountiesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Counties
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _uow.County.GetAllAsync();
            return View(res);
        }

        [AllowAnonymous]
        // GET: Counties/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _uow.County
                .FirstOrDefaultAsync(id.Value);

            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // GET: Counties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Counties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(County county)
        {
            if (!ModelState.IsValid) return View(county);

            _uow.County.Add(county);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Counties/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _uow.County.FirstOrDefaultAsync(id.Value);
            if (county == null)
            {
                return NotFound();
            }
            return View(county);
        }

        // POST: Counties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, County county)
        {
            if (id != county.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(county);

            _uow.County.Update(county);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Counties/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var county = await _uow.County.FirstOrDefaultAsync(id.Value);
            if (county == null)
            {
                return NotFound();
            }

            return View(county);
        }

        // POST: Counties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.County.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
