using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class CityService : BaseEntityService<IAppUnitOfWork, ICityRepository, City>, ICityService
    {
        public CityService(IAppUnitOfWork serviceUow, ICityRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }
    }
}