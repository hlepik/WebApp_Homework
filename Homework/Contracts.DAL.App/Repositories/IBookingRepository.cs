using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DTO.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>, IBookingRepositoryCustom<Booking>
    {
        // add your Booking custom method declarations here

    }
    public interface IBookingRepositoryCustom<TEntity>
    {

        Task<IEnumerable<BookingDTO>> GetAllDTOAsync(Guid userId, bool noTracking = true);
    }
}