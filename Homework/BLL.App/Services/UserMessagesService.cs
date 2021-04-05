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
    public class UserMessagesService : BaseEntityService<IAppUnitOfWork, IUserMessagesRepository, BLLAppDTO.UserMessages, DALAppDTO.UserMessages>, IUserMessagesService
    {
        public UserMessagesService(IAppUnitOfWork serviceUow, IUserMessagesRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new UserMessagesMapper(mapper))
        {
        }
        public async Task<IEnumerable<BLLAppDTO.UserMessages>> GetAllMessagesAsync(Guid userId, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllMessagesAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public Task<Guid> GetId(string email)
        {
            return ServiceRepository.GetId(email);

        }

        public async Task<BLLAppDTO.UserMessages?> FirstOrDefaultUserMessagesAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
              return Mapper.Map(await ServiceRepository.FirstOrDefaultUserMessagesAsync(id, userId, noTracking));

        }
    }
}