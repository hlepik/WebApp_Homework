

using System;
using System.ComponentModel;
using DAL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels.Answers
{
    public class AnswerCreateEditViewModel
    {
        public Answer Answer { get; set; } = default!;
        public Guid? SelectedQuestion { get; set; }
        public bool IsPoll { get; set; }
        [DisplayName("Question")]
        public string? QuestionName { get; set; }
        public SelectList? QuestionSelectList { get; set; }
    }
}