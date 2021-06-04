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

        public IEnumerable<Question>? AllQuestions { get; set; }
        [DisplayName("Tested people")]
        public int PeopleCount { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Created at")]
        public string? CreatedAt { get; set; }
        public int QuestionsCount { get; set; }

        public int Percentage { get; set; }
    }


}