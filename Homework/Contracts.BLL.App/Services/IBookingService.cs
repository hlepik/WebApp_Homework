using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Domain.App;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;


namespace Contracts.BLL.App.Services
{
    public interface IBookingService : IBaseEntityService< BLLAppDTO.Booking,
        DALAppDTO.Booking>, IBookingRepositoryCustom<BLLAppDTO.Booking>
    {

    }


}