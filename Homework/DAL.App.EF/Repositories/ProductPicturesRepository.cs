using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class ProductPicturesRepository : BaseRepository<DAL.App.DTO.ProductPictures, ProductPictures, AppDbContext>,IProductPicturesRepository
    {
        public ProductPicturesRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProductPicturesMapper(mapper))
        {
        }
    }
}