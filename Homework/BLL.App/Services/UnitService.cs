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
    public class UnitService: BaseEntityService<IAppUnitOfWork, IUnitRepository, BLLAppDTO.Unit, DALAppDTO.Unit>, IUnitService
    {
        public UnitService(IAppUnitOfWork serviceUow, IUnitRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new UnitMapper(mapper))
        {
        }


    }
}