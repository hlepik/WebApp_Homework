using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;
using DTO.App;

namespace BLL.App.Services
{
    public class ProductMaterialService: BaseEntityService<IAppUnitOfWork, IProductMaterialRepository, ProductMaterial>, IProductMaterialService
    {
        public ProductMaterialService(IAppUnitOfWork serviceUow, IProductMaterialRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }

        public async Task<IEnumerable<ProductMaterialDTO>> GetAllProductMaterialsAsync(Guid userId, bool noTracking = true)
        {
            return await ServiceRepository.GetAllProductMaterialsAsync(userId, true);
        }
    }
}