using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IQuestionRepository: IBaseRepository<Question>, IQuestionRepositoryCustom<Question>
    {

    }

    public interface IQuestionRepositoryCustom<TEntity>
    {
        Task<string?> GetName(Guid id);

        Task<IEnumerable<TEntity>> GetAllWithIdAsync(Guid id);
    }

}