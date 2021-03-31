using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels.ProductMaterial;


namespace WebApp.Controllers
{
    public class ProductMaterialsController : Controller
    {

        private readonly IAppBLL _bll;

        public ProductMaterialsController(IAppBLL bll)
        {
            _bll = bll;

        }

        // GET: ProductMaterials
        public async Task<IActionResult> Index()
        {

            return View(await _bll.ProductMaterial.GetAllProductMaterialsAsync(User.GetUserId()!.Value));

        }

        // GET: ProductMaterials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _bll.ProductMaterial
                .FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

            if (productMaterial == null)
            {
                return NotFound();
            }

            return View(productMaterial);
        }

        // GET: ProductMaterials/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProductMaterialCreateEditViewModels();
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description));
            vm.MaterialSelectList = new SelectList(await _bll.Material.GetAllAsync(), nameof(Material.Id),
                nameof(Material.Name));
            return View(vm);
        }

        // POST: ProductMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductMaterialCreateEditViewModels vm)
        {
            if (ModelState.IsValid)
            {
                _bll.ProductMaterial.Add(vm.ProductMaterial);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description), vm.ProductMaterial.ProductId);
            vm.MaterialSelectList = new SelectList(await _bll.Material.GetAllAsync(), nameof(Material.Id),
                nameof(Material.Name), vm.ProductMaterial.ProductId);
            return View(vm);
        }

        // GET: ProductMaterials/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (productMaterial == null)
            {
                return NotFound();
            }

            var vm = new ProductMaterialCreateEditViewModels();
            vm.ProductMaterial = productMaterial;
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description), vm.ProductMaterial.ProductId);
            vm.MaterialSelectList = new SelectList(await _bll.Material.GetAllAsync(), nameof(Material.Id),
                nameof(Material.Name), vm.ProductMaterial.MaterialId);
            return View(vm);
        }

        // POST: ProductMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductMaterialCreateEditViewModels vm)
        {
            if (id != vm.ProductMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bll.ProductMaterial.Update(vm.ProductMaterial);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.ProductMaterial.ProductId);
            vm.MaterialSelectList = new SelectList(await _bll.Material.GetAllAsync(), nameof(Material.Id),
                nameof(Material.Name), vm.ProductMaterial.MaterialId);

            return View(vm);
        }

        // GET: ProductMaterials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (productMaterial == null)
            {
                return NotFound();
            }

            return View(productMaterial);
        }

        // POST: ProductMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ProductMaterial.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }

}
