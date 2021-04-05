using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ProductMapper : BaseMapper<BLL.App.DTO.Product, DAL.App.DTO.Product>, IBaseMapper<BLL.App.DTO.Product, DAL.App.DTO.Product>
    {
        public ProductMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}