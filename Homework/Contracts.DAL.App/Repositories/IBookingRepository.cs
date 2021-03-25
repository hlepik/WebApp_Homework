using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        // add your Booking custom method declarations here

    }
}