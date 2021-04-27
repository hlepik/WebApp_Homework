using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Products;
#pragma warning disable 1591

namespace WebApp.Controllers
{
   [Authorize]

    public class ProductsController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Product.GetAllProductsAsync(User.GetUserId()!.Value));
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id.Value);


            return View(product);

        }



        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProductCreateEditViewModels();
            vm.CitySelectList = new SelectList(await _bll.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name));
            vm.ConditionSelectList = new SelectList(await _bll.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description));
            vm.CountySelectList = new SelectList(await _bll.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name));
            vm.CategorySelectList = new SelectList(await _bll.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name));
            vm.UnitSelectList= new SelectList(await _bll.Unit.GetAllAsync(), nameof(Unit.Id),
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
                vm.Product.DateAdded = DateTime.Now.Date;
                _bll.Product.Add(vm.Product);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.CitySelectList = new SelectList(await _bll.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product.CityId);
            vm.CountySelectList = new SelectList(await _bll.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.ConditionSelectList = new SelectList(await _bll.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description), vm.Product.ConditionId);
            vm.CategorySelectList = new SelectList(await _bll.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);
            vm.UnitSelectList= new SelectList(await _bll.Unit.GetAllAsync(), nameof(Unit.Id),
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

            var product = await _bll.Product.FirstOrDefaultDTOAsync(id.Value);


            var vm = new ProductCreateEditViewModels();
            vm.Product = product!;
            vm.CitySelectList = new SelectList(await _bll.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product!.CityId);
            vm.CountySelectList = new SelectList(await _bll.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.ConditionSelectList = new SelectList(await _bll.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description), vm.Product.ConditionId);
            vm.CategorySelectList = new SelectList(await _bll.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);
            vm.UnitSelectList= new SelectList(await _bll.Unit.GetAllAsync(), nameof(Unit.Id),
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
                vm.Product.AppUserId = User.GetUserId()!.Value;
                _bll.Product.Update(vm.Product);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.CitySelectList = new SelectList(await _bll.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product.CityId);
            vm.CountySelectList = new SelectList(await _bll.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.ConditionSelectList = new SelectList(await _bll.Condition.GetAllAsync(), nameof(Condition.Id),
                nameof(Condition.Description), vm.Product.ConditionId);
            vm.CategorySelectList = new SelectList(await _bll.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);
            vm.UnitSelectList= new SelectList(await _bll.Unit.GetAllAsync(), nameof(Unit.Id),
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

            var product = await _bll.Product.FirstOrDefaultAsync(id.Value);

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            _bll.ProductMaterial.RemoveProductMaterialsAsync(id);
            _bll.UserBookedProducts.RemoveUserBookedProductsAsync(id);
            _bll.Booking.RemoveBookingAsync(id);
            _bll.Picture.RemovePictureAsync(id);
            _bll.Product.RemoveProductAsync(id);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}


