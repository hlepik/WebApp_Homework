using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class ProductPicturesService: BaseEntityService<IAppUnitOfWork, IProductPicturesRepository, ProductPictures>, IProductPicturesService
    {
        public ProductPicturesService(IAppUnitOfWork serviceUow, IProductPicturesRepository serviceRepository) : base(serviceUow,
            serviceRepository)
        {

        }


    }
}