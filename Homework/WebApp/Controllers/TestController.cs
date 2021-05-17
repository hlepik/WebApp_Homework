using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ViewModels.Test;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly AppDbContext _ctx;

        public TestController(ILogger<TestController> logger, AppDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        // GET
        public async  Task<IActionResult> Test()
        {
            _logger.LogInformation("Test method starts");
            var vm = new TestViewModel
            {
                Products = await _ctx
                    .Products
                    .Include(x => x.County)
                    .ThenInclude(x => x!.Name)
                    .ThenInclude(x => x!.Translations)
                    .Include(x => x.City)
                    .ThenInclude(x => x!.Name)
                    .ThenInclude(x => x!.Translations)
                    .Include(x => x.Category)
                    .ThenInclude(x => x!.Name)
                    .ThenInclude(x => x!.Translations)
                    .Include(x => x.Condition)
                    .ThenInclude(x => x!.Description)
                    .ThenInclude(x => x!.Translations)
                    .Include(x => x.ProductMaterials)
                    .ThenInclude(x => x.Material)
                    .ThenInclude(x => x!.Name)
                    .ThenInclude(x => x!.Translations)
                    .ToListAsync()
            };
            _logger.LogInformation("Test method pre-return");
            return View(vm);
        }

        [Authorize]
        public  string TestAuth()
        {
            return "OK";
        }
    }
}
