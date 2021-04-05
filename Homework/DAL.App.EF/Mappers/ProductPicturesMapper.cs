using AutoMapper;
using Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ProductPicturesMapper: BaseMapper<DAL.App.DTO.ProductPictures, Domain.App.ProductPictures>, IBaseMapper<DAL.App.DTO.ProductPictures, Domain.App.ProductPictures>
    {
        public ProductPicturesMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}