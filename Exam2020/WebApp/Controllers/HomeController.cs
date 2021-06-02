using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.ViewModels.Quizzes;
using Result = DAL.App.DTO.Result;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger, IAppUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }


        public async Task<IActionResult> Index(string searchName)
        {
            var res = await _uow.Quiz.GetAllAsync();
            if (!String.IsNullOrEmpty(searchName))
            {
                res = await _uow.Quiz.GetSearchResult(searchName);
            }

            return View(res);

        }


        // public async Task<IActionResult> Quiz(Guid id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var quiz = await _uow.Quiz
        //         .FirstOrDefaultAsync(id);
        //
        //
        //     if (quiz == null)
        //     {
        //         return NotFound();
        //     }
        //     //
        //     // return RedirectToAction("Create", "Results", new { id = id});
        //
        //
        // }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task<IActionResult> Create(Guid id)
        {

            var res = await _uow.Quiz.FirstOrDefaultAsync(id);

            var vm = new ResultCreateEditViewModel();
            vm.Questions = res!.Questions;
            vm.QuizId = res.Id;
            vm.Quiz = res.QuizName;

            return View(vm);

        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Guid> userAnswers, ResultCreateEditViewModel vm)
        {
            var points = 0;
            var correctAnswers = 0;

            var quiz = await _uow.Quiz.FirstOrDefaultAsync(vm.QuizId);
            correctAnswers = await _uow.Quiz.GetCorrectAnswers(vm.QuizId);
            foreach (var each in userAnswers)
            {
                var answer = await _uow.Answer.FirstOrDefaultAsync(each);
                var question = await _uow.Question.FirstOrDefaultAsync(answer!.QuestionId);

                if (!question!.IsPoll)
                {
                    if (!question!.MultipleChoice)
                    {

                        foreach (var correctAnswer in question!.Answers!)
                        {
                            if (each.Equals(correctAnswer.Id) && correctAnswer.IsAnswerCorrect)
                            {
                                points += 1;
                            }
                        }
                    }
                    else
                    {
                        foreach (var correctAnswer in question!.Answers!)
                        {
                            if (each.Equals(correctAnswer.Id) && correctAnswer.IsAnswerCorrect)
                            {
                                points++;
                            }
                            else if (each.Equals(correctAnswer.Id) && points > 0)
                            {
                                points--;
                            }
                        }

                    }
                }
                else
                {
                    points++;
                }
            }

            var quizResult = 100;
            if (points != 0 && correctAnswers != 0)
            {
                quizResult = (points * 100) /correctAnswers;

            }

            var user = User.GetUserId()!.Value;

            if (user != Guid.Empty)
            {
                var userScore = new Result
                {
                    AppUserId = user,
                    TotalAnswers = correctAnswers,
                    CorrectAnswersCount = points,
                    Percentage = quizResult,
                    QuizId = vm.QuizId
                };

                _uow.Result.Add(userScore);
                await _uow.SaveChangesAsync();
            }

            quiz!.PeopleCount += 1;
            quiz.Percentage = (quiz.Percentage + quizResult) / 2;
            _uow.Quiz.Update(quiz);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Details", "Home", new { total = points, percentage = quizResult});
        }

        public async Task<IActionResult> Details(int total, int percentage)
        {
            if (total == null)
            {
                return NotFound();
            }

            var vm = new ResultAnswersCreateEditViewModel();

            vm.Percentage = percentage;
            vm.CorrectAnswers = total;
            return View(vm);
        }

    }
}