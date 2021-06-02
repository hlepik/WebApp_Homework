using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ResultMapper : BaseMapper<DAL.App.DTO.Result, Domain.App.Result>,
        IBaseMapper<DAL.App.DTO.Result, Domain.App.Result>
    {
        public ResultMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}