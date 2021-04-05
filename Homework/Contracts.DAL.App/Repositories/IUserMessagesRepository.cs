
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using Domain.App.Identity;
using DALAppDTO = DAL.App.DTO;



namespace Contracts.DAL.App.Repositories
{
    public interface IUserMessagesRepository : IBaseRepository<UserMessages>, IUserMessagesRepositoryCustom<UserMessages>
    {

    }
    public interface  IUserMessagesRepositoryCustom<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllMessagesAsync(Guid userId, bool noTracking = true);
        Task<Guid> GetId(string email);

        Task<TEntity?> FirstOrDefaultUserMessagesAsync(Guid id, Guid userId = default,
            bool noTracking = true);
    }
}