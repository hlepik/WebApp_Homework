using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CityMapper : BaseMapper<BLL.App.DTO.City, DAL.App.DTO.City>, IBaseMapper<BLL.App.DTO.City, DAL.App.DTO.City>
    {
        public CityMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}