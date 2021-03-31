using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class ProductPicturesRepository : BaseRepository<ProductPictures, AppDbContext>,IProductPicturesRepository
    {
        public ProductPicturesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}