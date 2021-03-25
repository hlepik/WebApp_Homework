using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {

        //we dont have any custom methods yet
        //IBookingStatusRepository BookingStatus { get; }
        IProductRepository Product { get; }
        IBookingRepository Booking { get; }
        IUserBookedProductsRepository UserBookedProducts { get; }
        IBaseRepository<Category> Category { get; }
        IBaseRepository<City> City { get; }
        IBaseRepository<Condition> Condition { get; }
        IBaseRepository<County> County { get; }
        IBaseRepository<Material> Material { get; }
        IMessageFormRepository MessageForm { get; }
        IPictureRepository Picture { get; }
        IProductMaterialRepository ProductMaterial{ get; }
        IBaseRepository<Unit> Unit { get; }
        IBaseRepository<UserBookings> UserBookings { get; }
        IBaseRepository<UserMessages> UserMessages { get; }



    }
}