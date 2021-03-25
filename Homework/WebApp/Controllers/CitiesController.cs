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
    public class CitiesController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public CitiesController(IAppUnitOfWork uow)
        {

            _uow = uow;
        }

        // GET: Cities
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // var res = await _uow.City.GetAllAsync(User.GetUserId()!.Value);
            // return View(res);
            var res = await _uow.City.GetAllAsync();
            return View(res);


        }

        [AllowAnonymous]
        // GET: Cities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _uow.City
                .FirstOrDefaultAsync(id.Value);


            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            if (!ModelState.IsValid) return View(city);

            _uow.City.Add(city);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _uow.City.FirstOrDefaultAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(city);

            _uow.City.Update(city);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _uow.City.FirstOrDefaultAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.City.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
