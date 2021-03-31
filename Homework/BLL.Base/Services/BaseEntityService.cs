using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base;

namespace BLL.Base.Services
{
    public class BaseEntityService<TUnitOfWork, TRepository, TEntity>
        : BaseEntityService<TUnitOfWork, TRepository, TEntity, Guid>, IBaseEntityService<TEntity>
        where TEntity : class, IDomainEntityId
        where TUnitOfWork : IBaseUnitOfWork
        where TRepository : IBaseRepository<TEntity>
    {
        public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }
    }

    public class BaseEntityService<TUnitOfWork, TRepository, TEntity, TKey> : IBaseEntityService<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
        where TUnitOfWork : IBaseUnitOfWork
        where TRepository : IBaseRepository<TEntity, TKey>
    {
        protected TUnitOfWork ServiceUow;
        protected TRepository ServiceRepository;

        public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository)
        {
            ServiceUow = serviceUow;
            ServiceRepository = serviceRepository;
        }

        public TEntity Add(TEntity entity)
        {
            return ServiceRepository.Add(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return ServiceRepository.Update(entity);
        }

        public TEntity Remove(TEntity entity, TKey? userId = default)
        {
            return ServiceRepository.Remove(entity, userId);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllAsync(userId, noTracking));
        }

        public async Task<TEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
        {
            return await ServiceRepository.FirstOrDefaultAsync(id, userId, noTracking);
        }

        public async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            return await ServiceRepository.ExistsAsync(id, userId);
        }

        public async Task<TEntity> RemoveAsync(TKey id, TKey? userId = default)
        {
            return await ServiceRepository.RemoveAsync(id, userId);
        }
    }
}
