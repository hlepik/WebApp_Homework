using System;
using System.Collections.Generic;
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
    public class MessageFormService: BaseEntityService<IAppUnitOfWork, IMessageFormRepository, BLLAppDTO.MessageForm, DALAppDTO.MessageForm>, IMessageFormService
    {
        public MessageFormService(IAppUnitOfWork serviceUow, IMessageFormRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new MessageFormMapper(mapper))
        {
        }

        public async Task<BLLAppDTO.MessageForm?> FirstOrDefaultMessagesAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultMessagesAsync(id))!;
        }
    }
}