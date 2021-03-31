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
        public IUserBookingsRepository UserBookings => GetRepository(() => new UserBookingsRepository(UowDbContext));

        public IUserMessagesRepository UserMessages => GetRepository(() => new UserMessagesRepository(UowDbContext));

        public IBookingRepository Booking => GetRepository(() => new BookingRepository(UowDbContext));

        public IUserBookedProductsRepository UserBookedProducts => GetRepository(() => new UserBookedProductsRepository(UowDbContext));
        public ICategoryRepository Category => GetRepository(() => new CategoryRepository(UowDbContext));
        public ICityRepository City => GetRepository(() => new CityRepository(UowDbContext));
        public IConditionRepository Condition => GetRepository(() => new ConditionRepository(UowDbContext));
        public ICountyRepository County => GetRepository(() => new CountyRepository(UowDbContext));
        public IMaterialRepository Material => GetRepository(() => new MaterialRepository(UowDbContext));
        public IMessageFormRepository MessageForm => GetRepository(() => new MessageFormRepository(UowDbContext));
        public IPictureRepository Picture => GetRepository(() => new PictureRepository(UowDbContext));
        public IUnitRepository Unit => GetRepository(() => new UnitRepository(UowDbContext));

        public IProductMaterialRepository ProductMaterial => GetRepository(() => new ProductMaterialRepository(UowDbContext));

        public IProductRepository Product => GetRepository(() => new ProductRepository(UowDbContext));

        public IProductPicturesRepository ProductPictures => GetRepository(() => new ProductPicturesRepository(UowDbContext));


    }
}