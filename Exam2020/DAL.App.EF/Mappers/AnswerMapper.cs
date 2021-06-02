using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class AnswerMapper : BaseMapper<DAL.App.DTO.Answer, Domain.App.Answer>,
        IBaseMapper<DAL.App.DTO.Answer, Domain.App.Answer>
    {
        public AnswerMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}