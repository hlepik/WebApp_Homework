using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ConditionMapper: BaseMapper<DAL.App.DTO.Condition, Domain.App.Condition>,IBaseMapper<DAL.App.DTO.Condition, Domain.App.Condition>
    {
        public ConditionMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}