using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Condition = BLL.App.DTO.Condition;
#pragma warning disable 1591


namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConditionsController : Controller
    {

        private readonly IAppBLL _bll;

        public ConditionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Conditions

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var res = await _bll.Condition.GetAllAsync();
            return View(res);
        }

        [AllowAnonymous]
        // GET: Conditions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = await _bll.Condition
                .FirstOrDefaultAsync(id.Value);

            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
        }

        // GET: Conditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Condition condition)
        {
            if (!ModelState.IsValid) return View(condition);

            _bll.Condition.Add(condition);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Conditions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = await _bll.Condition.FirstOrDefaultAsync(id.Value);
            if (condition == null)
            {
                return NotFound();
            }
            return View(condition);
        }

        // POST: Conditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Condition condition)
        {
            if (id != condition.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(condition);

            _bll.Condition.Update(condition);
            await _bll.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Conditions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condition = await _bll.Condition.FirstOrDefaultAsync(id.Value);
            if (condition == null)
            {
                return NotFound();
            }

            return View(condition);
        }

        // POST: Conditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Condition.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
