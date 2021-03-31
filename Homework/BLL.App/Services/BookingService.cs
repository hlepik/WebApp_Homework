using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;
using DTO.App;

namespace BLL.App.Services
{
    public class BookingService: BaseEntityService<IAppUnitOfWork, IBookingRepository, Booking>, IBookingService
    {
        public BookingService(IAppUnitOfWork serviceUow, IBookingRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }


        public async Task<IEnumerable<BookingDTO>> GetAllDTOAsync(Guid userId, bool noTracking = true)
        {
            return await ServiceRepository.GetAllDTOAsync(userId);
        }
    }

}