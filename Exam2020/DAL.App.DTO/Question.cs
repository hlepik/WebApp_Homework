using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Question : DomainEntityId
    {
        [MaxLength(500), MinLength(2)]
        [DisplayName("Question")]
        public string QuestionText { get; set; } = default!;
        [DisplayName("Multiple Choice")]
        public bool MultipleChoice { get; set; }
        [DisplayName("Poll")]
        public bool IsPoll { get; set; }

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public ICollection<Domain.App.Answer>? Answers { get; set; }
    }
}