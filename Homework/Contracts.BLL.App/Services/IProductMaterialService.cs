using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductMaterialService:  IBaseEntityService< BLLAppDTO.ProductMaterial, DALAppDTO.ProductMaterial>, IProductMaterialRepositoryCustom<BLLAppDTO.ProductMaterial>
    {

    }
}