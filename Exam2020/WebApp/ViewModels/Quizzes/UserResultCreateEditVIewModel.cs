using DAL.App.DTO;

namespace WebApp.ViewModels.Quizzes
{
    public class UserResultCreateEditVIewModel
    {
        public Result Result { get; set; } = default!;
        public string? QuizName { get; set; }
    }
}