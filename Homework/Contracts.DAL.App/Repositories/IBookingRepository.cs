
using System;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>, IBookingRepositoryCustom<Booking>
    {

    }
    public interface IBookingRepositoryCustom<TEntity>
    {

        Task<TEntity> FirstOrDefaultDTOAsync(Guid id, Guid userId = default,
            bool noTracking = true);
    }
}