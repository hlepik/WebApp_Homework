using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class MessageFormService: BaseEntityService<IAppUnitOfWork, IMessageFormRepository, MessageForm>, IMessageFormService
    {
        public MessageFormService(IAppUnitOfWork serviceUow, IMessageFormRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }

    }
}