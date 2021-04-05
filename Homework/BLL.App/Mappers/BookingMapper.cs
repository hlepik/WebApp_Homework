using AutoMapper;
using Contracts.DAL.Base.Mappers;


namespace BLL.App.Mappers
{
    public class BookingMapper : BaseMapper<BLL.App.DTO.Booking, DAL.App.DTO.Booking>, IBaseMapper<BLL.App.DTO.Booking, DAL.App.DTO.Booking>

    {
        public BookingMapper(IMapper mapper) : base(mapper)
        {
        }
    }

}