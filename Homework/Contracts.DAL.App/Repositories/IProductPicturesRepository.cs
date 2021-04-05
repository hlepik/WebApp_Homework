using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IProductPicturesRepository  : IBaseRepository<ProductPictures>,
        IProductPicturesRepositoryCustom<ProductPictures>
    {

    }

    public interface IProductPicturesRepositoryCustom<TEntity>
    {
    }
}