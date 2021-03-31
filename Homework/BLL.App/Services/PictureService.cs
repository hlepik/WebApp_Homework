using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class PictureService: BaseEntityService<IAppUnitOfWork, IPictureRepository, Picture>, IPictureService
    {
        public PictureService(IAppUnitOfWork serviceUow, IPictureRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }
    }
}