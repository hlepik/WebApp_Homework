using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class MessageFormMapper: BaseMapper<DAL.App.DTO.MessageForm, Domain.App.MessageForm>,IBaseMapper<DAL.App.DTO.MessageForm, Domain.App.MessageForm>
    {
        public MessageFormMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}