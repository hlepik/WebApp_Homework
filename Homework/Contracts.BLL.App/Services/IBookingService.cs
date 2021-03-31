using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Domain.App;
using DTO.App;

namespace Contracts.BLL.App.Services
{
    public interface IBookingService : IBaseEntityService<Booking>, IBookingRepository
    {

    }


}