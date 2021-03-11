using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {

        IProductRepository Product { get; }
        //we dont have any custom methods yet
        //IBookingStatusRepository BookingStatus { get; }

        IBaseRepository<Booking> Booking { get; }
        IBaseRepository<BookingStatus> BookingStatus { get; }
        IBaseRepository<Category> Category { get; }
        IBaseRepository<City> City { get; }
        IBaseRepository<Condition> Condition { get; }
        IBaseRepository<County> County { get; }
        IBaseRepository<Material> Material { get; }
        IBaseRepository<MessageForm> MessageForm { get; }
        IBaseRepository<Picture> Picture { get; }
        IBaseRepository<ProductMaterial> ProductMaterial{ get; }
        IBaseRepository<Unit> Unit { get; }
        IBaseRepository<UserBooking> UserBooking { get; }
        IBaseRepository<UserMessage> UserMessage{ get; }
        IBaseRepository<UserProducts> UserProducts{ get; }
        IBaseRepository<User> User{ get; }

    }
}