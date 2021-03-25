using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Products;

namespace WebApp.Controllers
{
   [Authorize]

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

            var res = await _uow.Product.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProductCreateEditViewModels();
            vm.CitySelectList = new SelectList(await _uow.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name));
            vm.ConditionSelectList = new SelectList(await _uow.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description));
            vm.CountySelectList = new SelectList(await _uow.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name));
            vm.CategorySelectList = new SelectList(await _uow.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name));
            vm.UnitSelectList= new SelectList(await _uow.Unit.GetAllAsync(), nameof(Unit.Id),
                nameof(Unit.Name));
            return View(vm);

        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateEditViewModels vm)
        {
            if (ModelState.IsValid)
            {
                vm.Product.AppUserId = User.GetUserId()!.Value;
                _uow.Product.Add(vm.Product);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AppUserId"] = new SelectList(await _uow.User.GetAllAsync(), "Id", "Firstname", product.AppUserId);
            vm.CitySelectList = new SelectList(await _uow.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product.CityId);
            vm.CountySelectList = new SelectList(await _uow.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.ConditionSelectList = new SelectList(await _uow.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description), vm.Product.ConditionId);
            vm.CategorySelectList = new SelectList(await _uow.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);
            vm.UnitSelectList= new SelectList(await _uow.Unit.GetAllAsync(), nameof(Unit.Id),
                nameof(Unit.Name), vm.Product.UnitId);
            return View(vm);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (product == null)
            {
                return NotFound();
            }
            var vm = new ProductCreateEditViewModels();
            vm.Product = product;
            vm.CitySelectList = new SelectList(await _uow.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product.CityId);
            vm.CountySelectList = new SelectList(await _uow.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.ConditionSelectList = new SelectList(await _uow.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description), vm.Product.ConditionId);
            vm.CategorySelectList = new SelectList(await _uow.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);
            vm.UnitSelectList= new SelectList(await _uow.Unit.GetAllAsync(), nameof(Unit.Id),
                nameof(Unit.Name), vm.Product.UnitId);
            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductCreateEditViewModels vm)
        {
            if (id != vm.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Product.Update(vm.Product);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.CitySelectList = new SelectList(await _uow.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product.CityId);
            vm.CountySelectList = new SelectList(await _uow.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.ConditionSelectList = new SelectList(await _uow.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description), vm.Product.ConditionId);
            vm.CategorySelectList = new SelectList(await _uow.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);
            vm.UnitSelectList= new SelectList(await _uow.Unit.GetAllAsync(), nameof(Unit.Id),
                nameof(Unit.Name), vm.Product.UnitId);
            return View(vm);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _uow.Product.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
            await _uow.Product.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}


