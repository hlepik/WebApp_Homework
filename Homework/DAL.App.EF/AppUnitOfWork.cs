using System;
using System.Collections.Generic;
using AutoMapper;
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
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }

        public IUserMessagesRepository UserMessages => GetRepository(() => new UserMessagesRepository(UowDbContext, Mapper));

        public IBookingRepository Booking => GetRepository(() => new BookingRepository(UowDbContext, Mapper));

        public IUserBookedProductsRepository UserBookedProducts => GetRepository(() => new UserBookedProductsRepository(UowDbContext, Mapper));
        public ICategoryRepository Category => GetRepository(() => new CategoryRepository(UowDbContext, Mapper));
        public ICityRepository City => GetRepository(() => new CityRepository(UowDbContext, Mapper));
        public IConditionRepository Condition => GetRepository(() => new ConditionRepository(UowDbContext, Mapper));
        public ICountyRepository County => GetRepository(() => new CountyRepository(UowDbContext, Mapper));
        public IMaterialRepository Material => GetRepository(() => new MaterialRepository(UowDbContext, Mapper));
        public IMessageFormRepository MessageForm => GetRepository(() => new MessageFormRepository(UowDbContext, Mapper));
        public IPictureRepository Picture => GetRepository(() => new PictureRepository(UowDbContext, Mapper));
        public IUnitRepository Unit => GetRepository(() => new UnitRepository(UowDbContext, Mapper));

        public IProductMaterialRepository ProductMaterial => GetRepository(() => new ProductMaterialRepository(UowDbContext, Mapper));

        public IProductRepository Product => GetRepository(() => new ProductRepository(UowDbContext, Mapper));

        public IProductPicturesRepository ProductPictures => GetRepository(() => new ProductPicturesRepository(UowDbContext, Mapper));


    }
}