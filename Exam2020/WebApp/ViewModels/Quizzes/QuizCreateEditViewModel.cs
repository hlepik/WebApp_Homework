using System;
using System.ComponentModel;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Question = DAL.App.DTO.Question;

namespace WebApp.ViewModels.Quizzes
{
    public class QuizCreateEditViewModel
    {
        public Question Question { get; set; } = default!;
        public Guid? SelectedQuiz { get; set; }
        [DisplayName("Quiz name")]
        public string? QuizName { get; set; }
        public SelectList? QuizSelectList { get; set; }
    }
}