using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ProductMaterialMapper : BaseMapper<DAL.App.DTO.ProductMaterial, Domain.App.ProductMaterial>, IBaseMapper<DAL.App.DTO.ProductMaterial, Domain.App.ProductMaterial>
    {
        public ProductMaterialMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}