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
    public class ProductService: BaseEntityService<IAppUnitOfWork, IProductRepository, Product>, IProductService
    {
        public ProductService(IAppUnitOfWork serviceUow, IProductRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }

        public async Task<Product> ChangeBookingStatus(Guid id)
        {
            return await ServiceRepository.ChangeBookingStatus(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(Guid? userId = default, bool noTracking = true)
        {
            return await ServiceRepository.GetAllProductsAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsIsNotBookedAsync()
        {
            return await ServiceRepository.GetAllProductsIsNotBookedAsync();
        }

        public async Task<Product> FirstOrDefaultWithoutOutIdAsync(Guid id)
        {
            return await ServiceRepository.FirstOrDefaultWithoutOutIdAsync(id);
        }
    }
}