using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface ICountyRepository : IBaseRepository<County>,
        ICountyRepositoryCustom<County>
    {
    }

    public interface ICountyRepositoryCustom<TEntity>
    {
    }
}