using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using City = BLL.App.DTO.City;
#pragma warning disable 1591


namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {

        private readonly IAppBLL _bll;

        public CitiesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Cities
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            return View(await _bll.City.GetAllAsync());


        }

        [AllowAnonymous]
        // GET: Cities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _bll.City
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

            _bll.City.Add(city);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _bll.City.FirstOrDefaultAsync(id.Value);
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

            _bll.City.Update(city);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _bll.City.FirstOrDefaultAsync(id.Value);
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
            await _bll.City.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
