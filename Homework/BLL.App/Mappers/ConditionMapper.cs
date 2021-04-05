using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ConditionMapper : BaseMapper<BLL.App.DTO.Condition, DAL.App.DTO.Condition>, IBaseMapper<BLL.App.DTO.Condition, DAL.App.DTO.Condition>
    {
        public ConditionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}