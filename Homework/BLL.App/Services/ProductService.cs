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
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ProductService: BaseEntityService<IAppUnitOfWork, IProductRepository, BLLAppDTO.Product, DALAppDTO.Product>, IProductService
    {
        public ProductService(IAppUnitOfWork serviceUow, IProductRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProductMapper(mapper))
        {
        }

        public async  Task<BLLAppDTO.Product> ChangeBookingStatus(Guid id)
        {
            return Mapper.Map(await ServiceRepository.ChangeBookingStatus(id))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Product>> GetAllProductsIsNotBookedAsync()
        {
            return (await ServiceRepository.GetAllProductsIsNotBookedAsync()).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.Product> FirstOrDefaultDTOAsync(Guid id)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultDTOAsync(id))!;
        }

        public async Task<BLLAppDTO.Product> FirstOrDefaultWithoutOutIdAsync(Guid id)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultWithoutOutIdAsync(id))!;
        }

        public async Task<IEnumerable<BLLAppDTO.Product>> GetAllProductsAsync(Guid userId = default, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllProductsAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;
        }
    }
}