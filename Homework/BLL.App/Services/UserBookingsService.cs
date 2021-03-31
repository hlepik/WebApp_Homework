using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class UserBookingsService: BaseEntityService<IAppUnitOfWork, IUserBookingsRepository, UserBookings>, IUserBookingsService
    {
        public UserBookingsService(IAppUnitOfWork serviceUow, IUserBookingsRepository serviceRepository) : base(serviceUow,
            serviceRepository)
        {

        }


    }
}