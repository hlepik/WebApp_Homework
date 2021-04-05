using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;
using DTO.App;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ProductMaterialService: BaseEntityService<IAppUnitOfWork, IProductMaterialRepository, BLLAppDTO.ProductMaterial, DALAppDTO.ProductMaterial>, IProductMaterialService
    {
        public ProductMaterialService(IAppUnitOfWork serviceUow, IProductMaterialRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProductMaterialMapper(mapper))
        {
        }


        public async Task<BLLAppDTO.ProductMaterial> FirstOrDefaultDTOAsync(Guid id, Guid userId,
            bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultDTOAsync(id, userId, noTracking))!;

        }

        public async Task<IEnumerable<BLLAppDTO.ProductMaterial>> GetAllProductMaterialsAsync(Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllProductMaterialsAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;

        }
    }
}