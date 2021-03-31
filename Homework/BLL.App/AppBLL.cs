using System;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using Domain.App.Identity;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public IProductService Product =>
            GetService<IProductService>(() => new ProductService(Uow, Uow.Product));

        public IProductPicturesService ProductPictures =>
            GetService<IProductPicturesService>(() => new ProductPicturesService(Uow, Uow.ProductPictures));
        public IBookingService Booking =>
            GetService<IBookingService>(() => new BookingService(Uow, Uow.Booking));
        public IUserBookedProductsService UserBookedProducts =>
            GetService<IUserBookedProductsService>(() => new UserBookedProductsService(Uow, Uow.UserBookedProducts));

        public IMessageFormService MessageForm =>
            GetService<IMessageFormService>(() => new MessageFormService(Uow, Uow.MessageForm));
        public IPictureService Picture =>
            GetService<IPictureService>(() => new PictureService(Uow, Uow.Picture));
        public IProductMaterialService ProductMaterial =>
            GetService<IProductMaterialService>(() => new ProductMaterialService(Uow, Uow.ProductMaterial));

        public ICategoryService Category =>
            GetService<ICategoryService>(() => new CategoryService(Uow, Uow.Category));

        public ICityService City =>
            GetService<ICityService>(() => new CityService(Uow, Uow.City));

        public IConditionService Condition =>
            GetService<IConditionService>(() => new ConditionService(Uow, Uow.Condition));

        public ICountyService County =>
            GetService<ICountyService>(() => new CountyService(Uow, Uow.County));

        public IMaterialService Material =>
            GetService<IMaterialService>(() => new MaterialService(Uow, Uow.Material));

        public IUnitService Unit =>
            GetService<IUnitService>(() => new UnitService(Uow, Uow.Unit));

        public IUserBookingsService UserBookings =>
            GetService<IUserBookingsService>(() => new UserBookingsService(Uow, Uow.UserBookings));

        public IUserMessagesService UserMessages =>
            GetService<IUserMessagesService>(() => new UserMessagesService(Uow, Uow.UserMessages));
    }
}