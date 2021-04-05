using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class UnitMapper : BaseMapper<BLL.App.DTO.Unit, DAL.App.DTO.Unit>, IBaseMapper<BLL.App.DTO.Unit, DAL.App.DTO.Unit>
    {
        public UnitMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}