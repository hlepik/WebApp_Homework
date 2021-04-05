using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CityMapper: BaseMapper<DAL.App.DTO.City, Domain.App.City>, IBaseMapper<DAL.App.DTO.City, Domain.App.City>
    {
        public CityMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}