using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class MessageFormMapper : BaseMapper<BLL.App.DTO.MessageForm, DAL.App.DTO.MessageForm>, IBaseMapper<BLL.App.DTO.MessageForm, DAL.App.DTO.MessageForm>
    {
        public MessageFormMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}