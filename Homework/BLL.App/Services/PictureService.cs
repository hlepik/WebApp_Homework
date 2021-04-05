using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PictureService: BaseEntityService<IAppUnitOfWork, IPictureRepository, BLLAppDTO.Picture, DALAppDTO.Picture>, IPictureService

    {
        public PictureService(IAppUnitOfWork serviceUow, IPictureRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new PictureMapper(mapper))
        {
        }

        public async Task<IEnumerable<BLLAppDTO.Picture>> GetAllPicturesAsync(Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllPicturesAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;


        }

    }
}