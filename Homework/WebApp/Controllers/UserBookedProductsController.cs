using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.UserBookedProducts;
#pragma warning disable 1591

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
            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultBookedProductsAsync(id.Value, User.GetUserId()!.Value);


            if (userBookedProducts == null)
            {
                return NotFound();
            }

            var product = await _bll.Product.FirstOrDefaultDTOAsync(userBookedProducts.ProductId);

            return View(product);
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
            // vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
            //     nameof(Product.Description), vm.UserBookedProducts.ProductId);
            return View(vm);
        }

        // GET: UserBookedProducts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBookedProducts = await _bll.UserBookedProducts.FirstOrDefaultBookedProductsAsync(id.Value, User.GetUserId()!.Value);
            if (userBookedProducts == null)
            {
                return NotFound();
            }
            var vm = new UserBookedProductsCreateEditViewModel();
            vm.UserBookedProducts = userBookedProducts;
            // vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
            //     nameof(Product.Description), vm.UserBookedProducts.ProductId);
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
            // vm.ProductSelectList = new SelectList(await _bll.Product.GetAllAsync(), nameof(Product.Id),
            //     nameof(Product.Description), vm.UserBookedProducts.ProductId);
            return View(vm);
        }



        // POST: UserBookedProducts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productId = await _bll.UserBookedProducts.GetId(id);
            var bookingStatus = await _bll.Product.ChangeBookingStatus(productId);
            bookingStatus.IsBooked = false;
            _bll.Product.Update(bookingStatus);
            _bll.UserBookedProducts.RemoveUserBookedProductsAsync(productId);
            _bll.Booking.RemoveBookingAsync(productId);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
