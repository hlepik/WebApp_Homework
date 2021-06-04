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
using Extensions.Base;
using WebApp.ViewModels.Quizzes;
using Question = DAL.App.DTO.Question;

namespace WebApp.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public QuestionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {

            var res = await _uow.Question.GetAllAsync();
            return View(res);

        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _uow.Question.FirstOrDefaultAsync(id.Value);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public async Task<IActionResult> Create(Guid? id)
        {
            var vm = new QuizCreateEditViewModel();
            if (id != null)
            {
                vm.SelectedQuiz = id;
                vm.QuizName = await _uow.Quiz.GetName(id.Value);
            }

            vm.QuizSelectList = new SelectList(await _uow.Quiz.GetAllAsync(), nameof(Quiz.Id),
                nameof(Quiz.QuizName));
            return View(vm);

        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizCreateEditViewModel vm)
        {
            if (vm.SelectedQuiz != null)
            {
                vm.Question.QuizId = (Guid) vm.SelectedQuiz;
            }

            if (ModelState.IsValid)
            {
                _uow.Question.Add(vm.Question);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.QuizSelectList = new SelectList(await _uow.Quiz.GetAllAsync(), nameof(Quiz.Id),
                nameof(Quiz.QuizName), vm.Question.QuizId);
            return View(vm);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _uow.Question.FirstOrDefaultAsync(id.Value);
            if (question == null)
            {
                return NotFound();
            }
            var vm = new QuizCreateEditViewModel();
            vm.Question = question;
            vm.QuizSelectList = new SelectList(await _uow.Quiz.GetAllAsync(User.GetUserId()!.Value), nameof(Quiz.Id),
                nameof(Quiz.QuizName), vm.Question.QuizId);
            return View(vm);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  QuizCreateEditViewModel vm)
        {
            if (id != vm.Question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.Question.Update(vm.Question);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.QuizSelectList = new SelectList(await _uow.Quiz.GetAllAsync(User.GetUserId()!.Value), nameof(Quiz.Id),
                nameof(Quiz.QuizName), vm.Question.QuizId);
            return View(vm);
        }

        public async Task<IActionResult> Redirect(Guid id)
        {


            var quiz = await _uow.Question
                .FirstOrDefaultAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Answers", new { id = id});

        }

        // POST: Questions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var question = await _uow.Question.FirstOrDefaultAsync(id);

            if (question != null)
            {
                _uow.Answer.RemoveAnswerAsync(question.Id);
            }


            await _uow.Question.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
