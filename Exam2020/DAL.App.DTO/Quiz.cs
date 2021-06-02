using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Quiz: DomainEntityId
    {
        [MaxLength(500), MinLength(2)]
        [DisplayName("Quiz name")]
        public string QuizName { get; set; } = default!;

        public ICollection<Question>? Questions { get; set; }
        [DisplayName("Tested people")]
        public int PeopleCount { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int TotalAnswers { get; set; }

        public ICollection<AppUser>? AppUsers { get; set; }

        public ICollection<Answer>? Answers { get; set; }
        public int Percentage { get; set; }
        public ICollection<Result>? Results { get; set; }

    }
}