using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IUserBookedProductsRepository : IBaseRepository<UserBookedProducts>,
        IUserBookedProductsRepositoryCustom<UserBookedProducts>
    {

    }

    public interface IUserBookedProductsRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllBookedProductsAsync(Guid userId, bool noTracking = true);

        Task<TEntity?> FirstOrDefaultBookedProductsAsync(Guid id, Guid userId = default,
            bool noTracking = true);

        Task<Guid> GetId(Guid id);
        void RemoveUserBookedProductsAsync(Guid? id, Guid userId = default);
    }

}