using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class CountyMapper : BaseMapper<BLL.App.DTO.County, DAL.App.DTO.County>, IBaseMapper<BLL.App.DTO.County, DAL.App.DTO.County>
    {
        public CountyMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}