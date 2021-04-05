using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class MaterialMapper: BaseMapper<DAL.App.DTO.Material, Domain.App.Material>, IBaseMapper<DAL.App.DTO.Material, Domain.App.Material>
    {
        public MaterialMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}