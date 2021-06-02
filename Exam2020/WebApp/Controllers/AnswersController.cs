using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using WebApp.ViewModels.Answers;
using Question = DAL.App.DTO.Question;

namespace WebApp.Controllers
{
    public class AnswersController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public AnswersController( IAppUnitOfWork uow)
        {

            _uow = uow;
        }

        // GET: Answers
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Answer.GetAllAsync();
            return View(res);
        }

        // GET: Answers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _uow.Answer.FirstOrDefaultAsync(id.Value);
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: Answers/Create
        public  async Task<IActionResult> Create(Guid? id)
        {
            var vm = new AnswerCreateEditViewModel();
            var question = new Question();
            if (id != null)
            {
                question = await _uow.Question.FirstOrDefaultAsync(id.Value);

            }
            if (id != null)
            {
                vm.SelectedQuestion = id;
                vm.IsPoll = question!.IsPoll;
                vm.QuestionName = question.QuestionText;
            }

            vm.QuestionSelectList = new SelectList(await _uow.Question.GetAllAsync(), nameof(Question.Id),
                nameof(Question.QuestionText));
            return View(vm);
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnswerCreateEditViewModel vm)
        {
            if (vm.SelectedQuestion != null)
            {
                vm.Answer.QuestionId= (Guid) vm.SelectedQuestion;
            }

            if (ModelState.IsValid)
            {
                _uow.Answer.Add(vm.Answer);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.QuestionSelectList = new SelectList(await _uow.Question.GetAllAsync(), nameof(Question.Id),
                nameof(Question.QuestionText), vm.Answer.QuestionId);
            return View(vm);
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _uow.Answer.FirstOrDefaultAsync(id.Value);
            if (answer == null)
            {
                return NotFound();
            }
            var vm = new AnswerCreateEditViewModel();
            vm.Answer = answer;
            vm.QuestionSelectList = new SelectList(await _uow.Question.GetAllAsync(), nameof(Question.Id),
                nameof(Question.QuestionText), vm.Answer.QuestionId);
            return View(vm);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AnswerCreateEditViewModel vm)
        {
            if (id != vm.Answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Answer.Update(vm.Answer);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.QuestionSelectList = new SelectList(await _uow.Question.GetAllAsync(), nameof(Question.Id),
                nameof(Question.QuestionText), vm.Answer.QuestionId);
            return View(vm);
        }



        // POST: Answers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Answer.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
