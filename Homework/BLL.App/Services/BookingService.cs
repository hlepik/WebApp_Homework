
using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class BookingService: BaseEntityService<IAppUnitOfWork, IBookingRepository, BLLAppDTO.Booking, DALAppDTO.Booking>, IBookingService
    {
        public BookingService(IAppUnitOfWork serviceUow, IBookingRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new BookingMapper(mapper))
        {
        }


        public async Task<BLLAppDTO.Booking> FirstOrDefaultDTOAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            return Mapper.Map(await ServiceRepository.FirstOrDefaultDTOAsync(id))!;
        }
    }

}
