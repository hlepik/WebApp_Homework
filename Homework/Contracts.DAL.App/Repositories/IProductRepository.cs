using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>

    {
        Task<Product> ChangeBookingStatus(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsIsNotBookedAsync();
        Task<Product> FirstOrDefaultWithoutOutIdAsync(Guid id);
    }
}