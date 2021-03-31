using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.UserBookedProducts;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserBookedProductsController : Controller
    {
        private readonly IAppBLL _bll;

        public UserBookedProductsController(IAppBLL bll)
        {
            _bll = bll;

        }

        // GET: UserBookedProducts
        public async Task<IActionResult> Index()
        {
            return View(await _bll.UserBookedProducts.GetAllAsync(User.GetUserId()!.Value));

        }

        // GET: UserBookedProducts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userBookedProducts = await _bll.Product.FirstOrDefaultWithoutOutIdAsync(id.Value);


            if (userBookedProducts == null)
            {
                return NotFound();
            }

            return View(userBookedProducts);
        }

        // GET: UserBookedProducts/Create
        public async Task<IActionResult> Create()
        {
            var vm = new UserBookedProductsCreateEditViewModel();
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(User.GetUserId()!.Value), nameof(Product.Id),
                nameof(Product.Description));
            return View(vm);
        }

        // POST: UserBookedProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserBookedProductsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _bll.UserBookedProducts.Add(vm.UserBookedProducts);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.UserBookedProducts.ProductId);
            return View(vm);
        }

        // GET: UserBookedProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (userBookedProducts == null)
            {
                return NotFound();
            }
            var vm = new UserBookedProductsCreateEditViewModel();
            vm.UserBookedProducts = userBookedProducts;
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.UserBookedProducts.ProductId);
            return View(vm);
        }

        // POST: UserBookedProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserBookedProductsCreateEditViewModel vm)
        {
            if (id != vm.UserBookedProducts.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                _bll.UserBookedProducts.Update(vm.UserBookedProducts);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
                nameof(Product.Description), vm.UserBookedProducts.ProductId);
            return View(vm);
        }

        // GET: UserBookedProducts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);

            if (userBookedProducts == null)
            {
                return NotFound();
            }

            return View(userBookedProducts);
        }

        // POST: UserBookedProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.UserBookedProducts.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
