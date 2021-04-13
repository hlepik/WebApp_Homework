using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
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

        public async Task<IActionResult> Index()
        {
            return View(await _bll.Product.GetAllProductsAsync());

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
    }
}