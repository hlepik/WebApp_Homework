using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PictureMapper : BaseMapper<BLL.App.DTO.Picture, DAL.App.DTO.Picture>, IBaseMapper<BLL.App.DTO.Picture, DAL.App.DTO.Picture>
    {
        public PictureMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}