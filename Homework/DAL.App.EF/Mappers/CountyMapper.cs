using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CountyMapper: BaseMapper<DAL.App.DTO.County, Domain.App.County>,IBaseMapper<DAL.App.DTO.County, Domain.App.County>
    {
        public CountyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}