using System;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base;

namespace Contracts.BLL.Base.Services
{

    public interface IBaseEntityService<TEntity> : IBaseEntityService<TEntity, Guid>
    where TEntity : class, IDomainEntityId
    {

    }
    public interface IBaseEntityService<TEntity, TKey> :IBaseService, IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}