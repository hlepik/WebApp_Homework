using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>,
        IProductRepositoryCustom<Product>

    {
    }
    public interface IProductRepositoryCustom<TEntity>
    {
        Task<TEntity> ChangeBookingStatus(Guid id);
        Task<IEnumerable<TEntity>> GetAllProductsIsNotBookedAsync();

        Task<TEntity> FirstOrDefaultDTOAsync(Guid id);
        Task<TEntity> FirstOrDefaultWithoutOutIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllProductsAsync(Guid userId = default, bool noTracking = true);
    }
}