using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface ICityRepository : IBaseRepository<City>,
        ICityRepositoryCustom<City>
    {

    }

    public interface ICityRepositoryCustom<TEntity>
    {
    }
}