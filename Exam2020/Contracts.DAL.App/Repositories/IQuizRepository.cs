using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IQuizRepository : IBaseRepository<Quiz>, IQuizRepositoryCustom<Quiz>
    {

    }

    public interface IQuizRepositoryCustom<TEntity>
    {

        Task<string?> GetName(Guid id);
        Task<IEnumerable<TEntity>> GetSearchResult(string searchName);
        Task<int> GetQuestionsCount(Guid id);
        Task<int> GetCorrectAnswers(Guid id);
    }

}