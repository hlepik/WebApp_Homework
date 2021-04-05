using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IPictureRepository : IBaseRepository<Picture>,
        IPictureRepositoryCustom<Picture>
    {

    }

    public interface IPictureRepositoryCustom<TEntity>
    {

        Task<IEnumerable<TEntity>> GetAllPicturesAsync(Guid userId, bool noTracking = true);
    }
}