using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class UserBookedProductsMapper: BaseMapper<DAL.App.DTO.UserBookedProducts, Domain.App.UserBookedProducts>, IBaseMapper<DAL.App.DTO.UserBookedProducts, Domain.App.UserBookedProducts>
    {
        public UserBookedProductsMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}