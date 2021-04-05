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
    public class MaterialService: BaseEntityService<IAppUnitOfWork, IMaterialRepository, BLLAppDTO.Material, DALAppDTO.Material>, IMaterialService
    {
        public MaterialService(IAppUnitOfWork serviceUow, IMaterialRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new MaterialMapper(mapper))
        {
        }

    }
}