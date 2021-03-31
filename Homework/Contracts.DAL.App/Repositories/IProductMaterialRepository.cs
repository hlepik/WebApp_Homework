using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DTO.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductMaterialRepository  : IBaseRepository<ProductMaterial>
    {

        Task<IEnumerable<ProductMaterialDTO>> GetAllProductMaterialsAsync(Guid userId, bool noTracking = true);

    }
}