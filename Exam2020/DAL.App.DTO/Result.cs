using System;
using System.ComponentModel;
using DAL.App.DTO.Identity;
using Domain.Base;

namespace DAL.App.DTO
{
    public class Result: DomainEntityId
    {
        [DisplayName("Correct answers")]
        public int CorrectAnswersCount { get; set; }

        public int TotalAnswers { get; set; }
        public int Percentage { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
    }
}