using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class MaterialService: BaseEntityService<IAppUnitOfWork, IMaterialRepository, Material>, IMaterialService
    {
        public MaterialService(IAppUnitOfWork serviceUow, IMaterialRepository serviceRepository) : base(serviceUow,
            serviceRepository)
        {

        }

    }
}