using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Services;
using Domain.App;
using Domain.App.Identity;


namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IProductService Product { get; }
        IBookingService Booking { get; }
        IUserBookedProductsService UserBookedProducts { get; }
        ICategoryService Category { get; }
        ICityService City { get; }
        IConditionService Condition { get; }
        ICountyService County { get; }
        IMaterialService Material { get; }
        IMessageFormService MessageForm { get; }
        IPictureService Picture { get; }
        IProductMaterialService ProductMaterial{ get; }
        IProductPicturesService ProductPictures { get; }
        IUnitService Unit { get; }
        IUserBookingsService UserBookings { get; }
        IUserMessagesService UserMessages { get; }
    }
}