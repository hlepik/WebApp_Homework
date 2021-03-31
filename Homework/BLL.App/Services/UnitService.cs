using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class UnitService: BaseEntityService<IAppUnitOfWork, IUnitRepository, Unit>, IUnitService
    {
        public UnitService(IAppUnitOfWork serviceUow, IUnitRepository serviceRepository) : base(serviceUow,
            serviceRepository)
        {

        }


    }
}