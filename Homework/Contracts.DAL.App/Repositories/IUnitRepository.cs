using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IUnitRepository : IBaseRepository<Unit>,
        IUnitRepositoryCustom<Unit>
    {

    }

    public interface IUnitRepositoryCustom<TEntity>
    {
    }
}