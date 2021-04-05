using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IConditionRepository : IBaseRepository<Condition>,
        IConditionRepositoryCustom<Condition>
    {
    }

    public interface IConditionRepositoryCustom<TEntity>
    {
    }
}