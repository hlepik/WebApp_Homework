using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IAnswerRepository : IBaseRepository<Answer>, IAnswerRepositoryCustom<Answer>
    {

    }
    public interface IAnswerRepositoryCustom<TEntity>
    {
        void RemoveAnswerAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAnswersAsync(Guid id);

    }
}