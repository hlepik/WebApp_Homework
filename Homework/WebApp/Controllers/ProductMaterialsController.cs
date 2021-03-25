using System;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly IAppUnitOfWork _uow;

        public ProductMaterialsController(IAppUnitOfWork uow)
        {

            _uow = uow;
        }

        // GET: ProductMaterials
        public async Task<IActionResult> Index()
        {

            var res = await _uow.ProductMaterial.GetAllAsync(User.GetUserId()!.Value);
            return View(res);
        }

        // GET: ProductMaterials/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _uow.ProductMaterial
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
            // ViewData["MaterialId"] = new SelectList(await _uow.Material.GetAllAsync(User.GetUserId()!.Value), "Id", "Name");
            // ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), "Id", "Description");
            var vm = new ProductMaterialCreateEditViewModels();
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description));
            vm.MaterialSelectList = new SelectList(await _uow.Material.GetAllAsync(), nameof(Material.Id),
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
                _uow.ProductMaterial.Add(vm.ProductMaterial);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["MaterialId"] = new SelectList(await _uow.Material.GetAllAsync(User.GetUserId()!.Value), "Id", "Name", productMaterial.MaterialId);
            // ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), "Id", "Description", productMaterial.ProductId);
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.ProductMaterial.ProductId);
            vm.MaterialSelectList = new SelectList(await _uow.Material.GetAllAsync(), nameof(Material.Id),
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

            var productMaterial = await _uow.ProductMaterial.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (productMaterial == null)
            {
                return NotFound();
            }
            // ViewData["MaterialId"] = new SelectList(await _uow.Material.GetAllAsync(User.GetUserId()!.Value), "Id", "Name", productMaterial.MaterialId);
            // ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), "Id", "Description", productMaterial.ProductId);
            var vm = new ProductMaterialCreateEditViewModels();
            vm.ProductMaterial = productMaterial;
            vm.ProductSelectList = new SelectList(await _uow.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.ProductMaterial.ProductId);
            vm.MaterialSelectList = new SelectList(await _uow.Material.GetAllAsync(), nameof(Material.Id),
                nameof(Material.Name), vm.ProductMaterial.MaterialId);
            return View(vm);
        }

        // POST: ProductMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ProductMaterial.Update(productMaterial);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaterialId"] = new SelectList(await _uow.Material.GetAllAsync(User.GetUserId()!.Value), "Id", "Name", productMaterial.MaterialId);
            ViewData["ProductId"] = new SelectList(await _uow.Product.GetAllAsync(User.GetUserId()!.Value), "Id", "Description", productMaterial.ProductId);

            return View();
        }

        // GET: ProductMaterials/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productMaterial = await _uow.ProductMaterial.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
            await _uow.ProductMaterial.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }

}
