
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductService: IBaseEntityService< BLLAppDTO.Product, DALAppDTO.Product>, IProductRepositoryCustom<BLLAppDTO.Product>
    {
        // Task<BLLAppDTO.Product> ChangeBookingStatus(Guid id);
        // Task<IEnumerable<BLLAppDTO.Product>> GetAllProductsIsNotBookedAsync();
        // Task<BLLAppDTO.Product> FirstOrDefaultWithoutOutIdAsync(Guid id);
        //
        // Task<IEnumerable<BLLAppDTO.Product>> GetAllProductsAsync(Guid? userId = default, bool noTracking = true);


    }
}