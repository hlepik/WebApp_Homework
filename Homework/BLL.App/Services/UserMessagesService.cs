using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App.Identity;
using DTO.App;


namespace BLL.App.Services
{
    public class UserMessagesService : BaseEntityService<IAppUnitOfWork, IUserMessagesRepository, UserMessages>, IUserMessagesService
    {

        public UserMessagesService(IAppUnitOfWork serviceUow, IUserMessagesRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }
        public async Task<IEnumerable<UserMessagesDTO>> GetAllMessagesAsync(string email)
        {
            return await ServiceRepository.GetAllMessagesAsync(email);
        }
    }
}