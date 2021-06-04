using System;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IResultRepository : IBaseRepository<Result>, IResultRepositoryCustom<Result>
    {

    }

    public interface IResultRepositoryCustom<TEntity>
    {
        void RemoveResultAsync(Guid id);
    }
}