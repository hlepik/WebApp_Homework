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
    public class CityService : BaseEntityService<IAppUnitOfWork, ICityRepository, BLLAppDTO.City, DALAppDTO.City>, ICityService
    {
        public CityService(IAppUnitOfWork serviceUow, ICityRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new CityMapper(mapper))
        {
        }
    }
}