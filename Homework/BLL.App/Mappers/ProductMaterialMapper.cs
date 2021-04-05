using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ProductMaterialMapper : BaseMapper<BLL.App.DTO.ProductMaterial, DAL.App.DTO.ProductMaterial>, IBaseMapper<BLL.App.DTO.ProductMaterial, DAL.App.DTO.ProductMaterial>
    {
        public ProductMaterialMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}