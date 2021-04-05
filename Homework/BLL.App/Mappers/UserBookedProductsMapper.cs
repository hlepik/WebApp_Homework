using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class UserBookedProductsMapper : BaseMapper<BLL.App.DTO.UserBookedProducts, DAL.App.DTO.UserBookedProducts>, IBaseMapper<BLL.App.DTO.UserBookedProducts, DAL.App.DTO.UserBookedProducts>
    {
        public UserBookedProductsMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}