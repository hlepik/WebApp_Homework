using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Answer: DomainEntityId
    {
        [MaxLength(128), MinLength(1)]
        public string? QuestionAnswer { get; set; }
        public bool IsAnswerCorrect { get; set; }

        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }

    }
}