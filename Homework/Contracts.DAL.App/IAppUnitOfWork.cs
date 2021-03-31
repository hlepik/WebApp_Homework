using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IProductRepository Product { get; }
        IBookingRepository Booking { get; }
        IUserBookedProductsRepository UserBookedProducts { get; }
        ICategoryRepository Category { get; }
        ICityRepository City { get; }
        IConditionRepository Condition { get; }
        ICountyRepository County { get; }
        IMaterialRepository Material { get; }
        IMessageFormRepository MessageForm { get; }
        IPictureRepository Picture { get; }
        IProductMaterialRepository ProductMaterial{ get; }
        IUnitRepository Unit { get; }
        IUserBookingsRepository UserBookings { get; }
        IUserMessagesRepository UserMessages { get; }
        IProductPicturesRepository ProductPictures { get; }



    }
}