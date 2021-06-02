using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Extensions.Base;
using WebApp.ViewModels.Quizzes;
using Answer = Domain.App.Answer;

namespace WebApp.Controllers
{
    public class ResultsController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public ResultsController( IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {

            var res = await _uow.Result.GetAllAsync(User.GetUserId()!.Value);

            return View(res);

        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _uow.Result.FirstOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public async Task<IActionResult> Create(Guid id)
        {

            var res = await _uow.Quiz.FirstOrDefaultAsync(id);

            var vm = new ResultCreateEditViewModel();



            return View(vm);

        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResultCreateEditViewModel vm)
        {

            var result = vm;

            return View(vm);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _uow.Result.FirstOrDefaultAsync(id.Value);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, DAL.App.DTO.Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(result);

            _uow.Result.Update(result);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _uow.Result.FirstOrDefaultAsync(id.Value);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Result.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
