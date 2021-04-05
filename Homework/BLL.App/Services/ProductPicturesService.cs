using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class ProductPicturesService : BaseEntityService<IAppUnitOfWork, IProductPicturesRepository, BLLAppDTO.ProductPictures, DALAppDTO.ProductPictures>, IProductPicturesService
    {
        public  ProductPicturesService(IAppUnitOfWork serviceUow, IProductPicturesRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new ProductPicturesMapper(mapper))
        {
        }


    }
}