using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DTO.App;

namespace Contracts.DAL.App.Repositories
{
    public interface ICategoryRepository :  IBaseRepository<Category>, ICategoryRepositoryCustom<Category>
    {

    }
    public interface ICategoryRepositoryCustom<TEntity>
    {

        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    }


}