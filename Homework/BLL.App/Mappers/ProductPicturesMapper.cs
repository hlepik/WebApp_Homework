using AutoMapper;
using Contracts.DAL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ProductPicturesMapper : BaseMapper<BLL.App.DTO.ProductPictures, DAL.App.DTO.ProductPictures>, IBaseMapper<BLL.App.DTO.ProductPictures, DAL.App.DTO.ProductPictures>
    {
        public ProductPicturesMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}