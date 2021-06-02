using System;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Result: DomainEntityId
    {

        public int CorrectAnswersCount { get; set; }
        public int TotalAnswers { get; set; }
        public int Percentage { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
    }
}