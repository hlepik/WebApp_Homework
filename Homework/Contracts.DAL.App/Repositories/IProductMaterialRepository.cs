using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IProductMaterialRepository  : IBaseRepository<ProductMaterial>,
        IProductMaterialRepositoryCustom<ProductMaterial>
    {
    }

    public interface IProductMaterialRepositoryCustom<TEntity>
    {
        Task<TEntity> FirstOrDefaultDTOAsync(Guid id, Guid userId, bool noTracking = true);
        Task<IEnumerable<TEntity>> GetAllProductMaterialsAsync(Guid userId, bool noTracking = true);
    }
}