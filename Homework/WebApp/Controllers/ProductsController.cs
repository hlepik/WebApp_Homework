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
    public class ProductsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Product.GetAllAsync();
            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _uow.Category.GetAllAsync(false), "Id", "Name");
            ViewData["ConditionId"] = new SelectList(await _uow.Condition.GetAllAsync(false), "Id", "Description");
            ViewData["CountyId"] = new SelectList(await _uow.County.GetAllAsync(false), "Id", "Name");
            ViewData["UnitId"] = new SelectList(await _uow.Unit.GetAllAsync(false), "Id", "Name");
            ViewData["City"] = new SelectList(await _uow.City.GetAllAsync(false), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _uow.Product.Add(product);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Category.GetAllAsync(), "Id", "Name", product.CategoryId);
            ViewData["ConditionId"] = new SelectList(await _uow.Condition.GetAllAsync(), "Id", "Description", product.ConditionId);
            ViewData["CountyId"] = new SelectList(await _uow.County.GetAllAsync(), "Id", "Name", product.CountyId);
            ViewData["UnitId"] = new SelectList(await _uow.Unit.GetAllAsync(), "Id", "Name", product.UnitId);
            ViewData["City"] = new SelectList(await _uow.City.GetAllAsync(), "Id", "Name", product.City);

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Category.GetAllAsync(), "Id", "Name", product.CategoryId);
            ViewData["ConditionId"] = new SelectList(await _uow.Condition.GetAllAsync(), "Id", "Description", product.ConditionId);
            ViewData["CountyId"] = new SelectList(await _uow.County.GetAllAsync(), "Id", "Name", product.CountyId);
            ViewData["UnitId"] = new SelectList(await _uow.Unit.GetAllAsync(), "Id", "Name", product.UnitId);
            ViewData["City"] = new SelectList(await _uow.City.GetAllAsync(), "Id", "Name", product.City);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Product.Update(product);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(await _uow.Category.GetAllAsync(), "Id", "Name", product.CategoryId);
            ViewData["ConditionId"] = new SelectList(await _uow.Condition.GetAllAsync(), "Id", "Description", product.ConditionId);
            ViewData["CountyId"] = new SelectList(await _uow.County.GetAllAsync(), "Id", "Name", product.CountyId);
            ViewData["UnitId"] = new SelectList(await _uow.Unit.GetAllAsync(), "Id", "Name", product.UnitId);
            ViewData["City"] = new SelectList(await _uow.City.GetAllAsync(), "Id", "Name", product.City);

            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Product.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(Guid id)
        {
            return await _uow.Product.ExistsAsync(id);
        }
    }
}
