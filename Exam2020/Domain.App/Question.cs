using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Question : DomainEntityId
    {
        [MaxLength(500), MinLength(2)]
        public string QuestionText { get; set; } = default!;

        public bool IsPoll { get; set; }
        public bool MultipleChoice { get; set; }

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public ICollection<Answer>? Answers { get; set; }
    }
}