using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base.Repositories
{

    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
        where TEntity : class, IDomainEntityId
    {
    }

    public interface IBaseRepository<TEntity, TKey> : IBaseRepositoryAsync<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey> // any more rules? maybe ID?
        where TKey : IEquatable<TKey>
    {
    }

}