using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ProductMapper: BaseMapper<DAL.App.DTO.Product, Domain.App.Product>,IBaseMapper<DAL.App.DTO.Product, Domain.App.Product>
    {
        public ProductMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}