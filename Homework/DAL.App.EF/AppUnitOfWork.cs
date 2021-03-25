using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {

        }
        public IBaseRepository<UserBookings> UserBookings => GetRepository(() => new BaseRepository<UserBookings, AppDbContext>(UowDbContext));

        public IBaseRepository<UserMessages> UserMessages => GetRepository(() => new BaseRepository<UserMessages, AppDbContext>(UowDbContext));

        public IBookingRepository Booking => GetRepository(() => new BookingRepository(UowDbContext));

        public IUserBookedProductsRepository UserBookedProducts => GetRepository(() => new UserBookedProductsRepository(UowDbContext));
        public IBaseRepository<Category> Category => GetRepository(() => new BaseRepository<Category, AppDbContext>(UowDbContext));
        public IBaseRepository<City> City => GetRepository(() => new BaseRepository<City, AppDbContext>(UowDbContext));
        public IBaseRepository<Condition> Condition => GetRepository(() => new BaseRepository<Condition, AppDbContext>(UowDbContext));
        public IBaseRepository<County> County => GetRepository(() => new BaseRepository<County, AppDbContext>(UowDbContext));
        public IBaseRepository<Material> Material => GetRepository(() => new BaseRepository<Material, AppDbContext>(UowDbContext));
        public IMessageFormRepository MessageForm => GetRepository(() => new MessageFormRepository(UowDbContext));
        public IPictureRepository Picture => GetRepository(() => new PictureRepository(UowDbContext));
        public IBaseRepository<Unit> Unit => GetRepository(() => new BaseRepository<Unit, AppDbContext>(UowDbContext));

        public IProductMaterialRepository ProductMaterial => GetRepository(() => new ProductMaterialRepository(UowDbContext));

        public IProductRepository Product => GetRepository(() => new ProductRepository(UowDbContext));



    }
}