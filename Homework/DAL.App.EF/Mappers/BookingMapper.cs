using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class BookingMapper: BaseMapper<DAL.App.DTO.Booking, Domain.App.Booking>, IBaseMapper<DAL.App.DTO.Booking, Domain.App.Booking>
    {
        public BookingMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}