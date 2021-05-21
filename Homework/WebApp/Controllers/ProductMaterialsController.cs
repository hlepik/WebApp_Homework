using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.ViewModels.ProductMaterial;
#pragma warning disable 1591


namespace WebApp.Controllers
{
    [Authorize]
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
                .FirstOrDefaultDTOAsync(id.Value, User.GetUserId()!.Value);


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

            var productMaterial = await _bll.ProductMaterial.FirstOrDefaultDTOAsync(id.Value, User.GetUserId()!.Value);


            var vm = new ProductMaterialCreateEditViewModels();
            vm.ProductMaterial = productMaterial!;
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description), vm.ProductMaterial!.ProductId);
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


        // POST: ProductMaterials/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _bll.ProductMaterial.RemoveProductMaterialsAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


    }

}
