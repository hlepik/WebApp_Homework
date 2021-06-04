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
using Microsoft.AspNetCore.Authorization;
using WebApp.ViewModels.Quizzes;
using Question = DAL.App.DTO.Question;
using Quiz = DAL.App.DTO.Quiz;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QuizzesController : Controller
    {
        private readonly IAppUnitOfWork _uow;


        public QuizzesController(IAppUnitOfWork uow)
        {
            _uow = uow;

        }

        [AllowAnonymous]
        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            var res = await _uow.Quiz.GetAllAsync();
            return View(res);

        }
        [AllowAnonymous]

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {

            var quiz = await _uow.Quiz
                .FirstOrDefaultAsync(id);


            if (quiz == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Questions", new { id = id});

        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DAL.App.DTO.Quiz quiz)
        {
            if (!ModelState.IsValid) return View(quiz);

            quiz.CreatedAt = DateTime.Now.ToShortDateString();
            quiz.Percentage = 100;
            quiz.PeopleCount = 0;
           _uow.Quiz.Add(quiz);
           await _uow.SaveChangesAsync();
           return RedirectToAction(nameof(Index));


        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _uow.Quiz.FirstOrDefaultAsync(id.Value);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(quiz);

            _uow.Quiz.Update(quiz);
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // // GET: Quizzes/Delete/5
        // public async Task<IActionResult> Delete(Guid? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var quiz = await _uow.Quiz.FirstOrDefaultAsync(id.Value);
        //     if (quiz == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return View(quiz);
        // }

        // POST: Quizzes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            var question = await _uow.Question.GetAllWithIdAsync(id);


            foreach (var each in question)
            {
                _uow.Answer.RemoveAnswerAsync(each.Id);
                await _uow.Question.RemoveAsync(each.Id);
            }


            _uow.Result.RemoveResultAsync(id);
            await _uow.Quiz.RemoveAsync(id);

            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
