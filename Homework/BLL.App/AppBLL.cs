using System;
using AutoMapper;
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
        protected IMapper Mapper;
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
        }

        public IProductService Product =>
            GetService<IProductService>(() => new ProductService(Uow, Uow.Product, Mapper));


        public IBookingService Booking =>
            GetService<IBookingService>(() => new BookingService(Uow, Uow.Booking, Mapper));
        public IUserBookedProductsService UserBookedProducts =>
            GetService<IUserBookedProductsService>(() => new UserBookedProductsService(Uow, Uow.UserBookedProducts, Mapper));

        public IMessageFormService MessageForm =>
            GetService<IMessageFormService>(() => new MessageFormService(Uow, Uow.MessageForm, Mapper));
        public IPictureService Picture =>
            GetService<IPictureService>(() => new PictureService(Uow, Uow.Picture, Mapper));
        public IProductMaterialService ProductMaterial =>
            GetService<IProductMaterialService>(() => new ProductMaterialService(Uow, Uow.ProductMaterial, Mapper));

        public ICategoryService Category =>
            GetService<ICategoryService>(() => new CategoryService(Uow, Uow.Category, Mapper));

        public ICityService City =>
            GetService<ICityService>(() => new CityService(Uow, Uow.City, Mapper));

        public IConditionService Condition =>
            GetService<IConditionService>(() => new ConditionService(Uow, Uow.Condition, Mapper));

        public ICountyService County =>
            GetService<ICountyService>(() => new CountyService(Uow, Uow.County, Mapper));

        public IMaterialService Material =>
            GetService<IMaterialService>(() => new MaterialService(Uow, Uow.Material, Mapper));

        public IUnitService Unit =>
            GetService<IUnitService>(() => new UnitService(Uow, Uow.Unit, Mapper));


        public IUserMessagesService UserMessages =>
            GetService<IUserMessagesService>(() => new UserMessagesService(Uow, Uow.UserMessages, Mapper));
    }
}