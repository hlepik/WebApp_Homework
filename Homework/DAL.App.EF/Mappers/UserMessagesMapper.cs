using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class UserMessagesMapper: BaseMapper<DAL.App.DTO.UserMessages, Domain.App.UserMessages>, IBaseMapper<DAL.App.DTO.UserMessages, Domain.App.UserMessages>
    {
        public UserMessagesMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}