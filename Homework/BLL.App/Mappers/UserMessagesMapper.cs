using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class UserMessagesMapper : BaseMapper<BLL.App.DTO.UserMessages, DAL.App.DTO.UserMessages>, IBaseMapper<BLL.App.DTO.UserMessages, DAL.App.DTO.UserMessages>
    {
        public UserMessagesMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}