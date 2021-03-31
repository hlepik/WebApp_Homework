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
    public class CategoryService: BaseEntityService<IAppUnitOfWork, ICategoryRepository, Category>, ICategoryService
    {


        public CategoryService(IAppUnitOfWork serviceUow, ICategoryRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }
        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await ServiceRepository.GetAllCategoriesAsync();
        }

    }
}