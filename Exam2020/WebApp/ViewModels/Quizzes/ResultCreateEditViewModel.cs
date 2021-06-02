using System;
using System.Collections.Generic;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Answer = Domain.App.Answer;

namespace WebApp.ViewModels.Quizzes
{
    public class ResultCreateEditViewModel
    {

        public string Quiz { get; set; } = default!;
        public Guid QuizId { get; set; } = default!;
        public ICollection<Question>? Questions { get; set; }
    }

}