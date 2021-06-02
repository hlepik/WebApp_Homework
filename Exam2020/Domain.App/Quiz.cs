using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Quiz: DomainEntityId
    {
        [MaxLength(500), MinLength(2)]
        public string QuizName { get; set; } = default!;
        public int Percentage { get; set; }
        public int PeopleCount { get; set; }
        public ICollection<Question>? Questions { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Result>? Results { get; set; }

    }
}