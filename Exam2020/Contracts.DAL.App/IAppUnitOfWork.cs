
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;


namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {

        IQuizRepository Quiz { get; }
        IAnswerRepository Answer { get; }
        IResultRepository Result { get; }
        IQuestionRepository Question { get; }


    }
}