using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IMaterialRepository : IBaseRepository<Material>,
        IMaterialRepositoryCustom<Material>
    {
    }

    public interface IMaterialRepositoryCustom<TEntity>
    {
    }
}