using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class UserBookedProductsService: BaseEntityService<IAppUnitOfWork, IUserBookedProductsRepository, UserBookedProducts>, IUserBookedProductsService
    {
        public UserBookedProductsService(IAppUnitOfWork serviceUow, IUserBookedProductsRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }
    }
}