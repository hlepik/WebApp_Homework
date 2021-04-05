using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class PictureMapper: BaseMapper<DAL.App.DTO.Picture, Domain.App.Picture>,IBaseMapper<DAL.App.DTO.Picture, Domain.App.Picture>
    {
        public PictureMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}