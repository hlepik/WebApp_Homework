using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.ViewModels.Booking;
using WebApp.ViewModels.Home;
using Booking = BLL.App.DTO.Booking;

#pragma warning disable 1591

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppBLL _bll;

        public HomeController(ILogger<HomeController> logger, IAppBLL bll)
        {
            _logger = logger;
            _bll = bll;
        }

        public async Task<IActionResult>  Index()
        {
            var vm = new HomePageViewModel();
            vm.LastInsertedProducts = await _bll.Product.GetLastInserted();
            vm.CitySelectList = new SelectList(await _bll.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name));
            vm.CountySelectList = new SelectList(await _bll.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name));
            vm.CategorySelectList = new SelectList(await _bll.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name));

            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(HomePageViewModel vm)
        {
            var product = await _bll.Product.GetSearchResult(vm.Product.CountyId, vm.Product.CityId,  vm.Product.CategoryId);


            vm.CitySelectList = new SelectList(await _bll.City.GetAllAsync(), nameof(City.Id),
                nameof(City.Name), vm.Product.CityId);
            vm.CountySelectList = new SelectList(await _bll.County.GetAllAsync(), nameof(County.Id),
                nameof(County.Name), vm.Product.CountyId);
            vm.CategorySelectList = new SelectList(await _bll.Category.GetAllAsync(), nameof(Category.Id),
                nameof(Category.Name), vm.Product.CategoryId);


            return View(product);
        }




        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddDays(1)
                }
            );
            return LocalRedirect(returnUrl);
        }

         public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Product.FirstOrDefaultDTOAsync(id.Value);


            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        public async Task<IActionResult> Reserve(Guid id)
        {
            var product = await _bll.Product.FirstOrDefaultDTOAsync(id);
            var vm = new BookingCreateEditViewModels();
            vm.Products = product!;
            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllProductsIsNotBookedAsync(), nameof(Product.Id),
                nameof(Product.Description));
            return View(vm);

        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(BookingCreateEditViewModels vm)
        {
            var product = await _bll.Product.ChangeBookingStatus(vm.Products.Id);


            if (ModelState.IsValid)
            {
                product.IsBooked = true;
                vm.Booking.ProductId = product.Id;
                vm.Booking.AppUserId = User.GetUserId()!.Value;
                vm.Booking.TimeBooked = DateTime.Now;

                _bll.Product.Update(product);

                var myBookings = new BLL.App.DTO.UserBookedProducts
                {
                    ProductId = product.Id,
                    AppUserId = User.GetUserId()!.Value
                };
                _bll.UserBookedProducts.Add(myBookings);
                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            vm.ProductSelectList = new SelectList(await _bll.Product.GetAllProductsIsNotBookedAsync(),
                nameof(Product.Id),
                nameof(Product.Description), vm.Booking.ProductId);

            return View();
            }
    }

}