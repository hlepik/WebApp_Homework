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

        public async Task<Guid> GetId(Guid id)
        {
            return await ServiceRepository.GetId(id);
        }

        public void RemovePictureAsync(Guid? id, Guid userId = default)
        {
            ServiceRepository.RemovePictureAsync(id);
        }

        public async Task<BLLAppDTO.Picture?> FirstOrDefaultDTOAsync(Guid id, Guid userId, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultDTOAsync(id, userId, noTracking))!;
        }
    }
}