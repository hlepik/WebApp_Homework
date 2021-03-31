using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class CountyService : BaseEntityService<IAppUnitOfWork, ICountyRepository, County>, ICountyService
    {
        public CountyService(IAppUnitOfWork serviceUow, ICountyRepository serviceRepository) : base(serviceUow,
            serviceRepository)
        {

        }

    }
}