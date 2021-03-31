using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Domain.App.Identity;

namespace Contracts.BLL.App.Services
{
    public interface IUserMessagesService : IBaseEntityService<UserMessages>, IUserMessagesRepository
    {

    }
}