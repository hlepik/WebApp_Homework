using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IMessageFormRepository: IBaseRepository<MessageForm>,
        IMessageFormRepositoryCustom<MessageForm>
    {

    }

    public interface IMessageFormRepositoryCustom<TEntity>
    {
        Task<TEntity?> FirstOrDefaultMessagesAsync(Guid id, Guid userId = default,
            bool noTracking = true);

        void RemoveMessagesAsync(Guid id);
    }
}