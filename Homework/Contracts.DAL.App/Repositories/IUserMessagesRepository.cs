using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using DTO.App;
using UserMessages = Domain.App.Identity.UserMessages;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserMessagesRepository : IBaseRepository<UserMessages>
    {
        Task<IEnumerable<UserMessagesDTO>> GetAllMessagesAsync(string email);
    }
}