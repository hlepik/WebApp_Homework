using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Services
{
    public class UserBookedProductsService: BaseEntityService<IAppUnitOfWork, IUserBookedProductsRepository, BLLAppDTO.UserBookedProducts, DALAppDTO.UserBookedProducts>, IUserBookedProductsService
    {
        public UserBookedProductsService(IAppUnitOfWork serviceUow, IUserBookedProductsRepository serviceRepository, IMapper mapper) : base(serviceUow, serviceRepository, new UserBookedProductsMapper(mapper))
        {
        }


        public async Task<IEnumerable<BLLAppDTO.UserBookedProducts>> GetAllBookedProductsAsync(Guid userId = default, bool noTracking = true)
        {
            return (await ServiceRepository.GetAllBookedProductsAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;
        }

        public async Task<BLLAppDTO.UserBookedProducts?> FirstOrDefaultBookedProductsAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
             return Mapper.Map(await ServiceRepository.FirstOrDefaultBookedProductsAsync(id, userId, noTracking));

        }
    }
}