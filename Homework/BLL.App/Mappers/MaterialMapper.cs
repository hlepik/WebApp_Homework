using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class MaterialMapper : BaseMapper<BLL.App.DTO.Material, DAL.App.DTO.Material>, IBaseMapper<BLL.App.DTO.Material, DAL.App.DTO.Material>
    {
        public MaterialMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}