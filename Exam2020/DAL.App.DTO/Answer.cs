using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Answer: DomainEntityId
    {
        [MaxLength(128), MinLength(1)]
        [DisplayName("Question answer")]
        public string? QuestionAnswer { get; set; }

        [DisplayName("Is answer correct?")]
        public bool IsAnswerCorrect { get; set; }
        [DisplayName("Question name")]
        public string? QuestionName { get; set; }

        public Guid QuestionId { get; set; }

        public Question? Question { get; set; }
    }
}