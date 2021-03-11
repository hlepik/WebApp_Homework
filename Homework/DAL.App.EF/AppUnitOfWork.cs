using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
            Product = new ProductRepository(uowDbContext);
            Booking = new BaseRepository<Booking, AppDbContext>(uowDbContext);
            BookingStatus = new BaseRepository<BookingStatus, AppDbContext>(uowDbContext);
            Category = new BaseRepository<Category, AppDbContext>(uowDbContext);
            City = new BaseRepository< City, AppDbContext>(uowDbContext);
            Condition = new BaseRepository<Condition, AppDbContext>(uowDbContext);
            County = new BaseRepository<County, AppDbContext>(uowDbContext);
            Material = new BaseRepository<Material, AppDbContext>(uowDbContext);
            MessageForm = new BaseRepository< MessageForm, AppDbContext>(uowDbContext);
            Picture = new BaseRepository<Picture, AppDbContext>(uowDbContext);
            ProductMaterial = new BaseRepository<ProductMaterial, AppDbContext>(uowDbContext);
            Unit = new BaseRepository<Unit, AppDbContext>(uowDbContext);
            UserBooking = new BaseRepository<UserBooking, AppDbContext>(uowDbContext);
            UserMessage = new BaseRepository<UserMessage , AppDbContext>(uowDbContext);
            UserProducts = new BaseRepository<UserProducts, AppDbContext>(uowDbContext);
            User = new BaseRepository<User, AppDbContext>(uowDbContext);

        }

        public IProductRepository Product { get; } = default!;
        public IBaseRepository<Booking> Booking { get; }
        public IBaseRepository<BookingStatus> BookingStatus { get; }
        public IBaseRepository<Category> Category { get; }
        public IBaseRepository<City> City { get; }
        public IBaseRepository<Condition> Condition { get; }
        public IBaseRepository<County> County { get; }
        public IBaseRepository<Material> Material { get; }
        public IBaseRepository<MessageForm> MessageForm { get; }
        public IBaseRepository<Picture> Picture { get; }
        public IBaseRepository<ProductMaterial> ProductMaterial { get; }
        public IBaseRepository<Unit> Unit { get; }
        public IBaseRepository<UserBooking> UserBooking { get; }
        public IBaseRepository<UserMessage> UserMessage { get; }
        public IBaseRepository<UserProducts> UserProducts { get; }
        public IBaseRepository<User> User { get; }
    }
}