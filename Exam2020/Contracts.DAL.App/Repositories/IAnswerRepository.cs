using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;


namespace Contracts.DAL.App.Repositories
{
    public interface IAnswerRepository : IBaseRepository<Answer>, IAnswerRepositoryCustom<Answer>
    {

    }
    public interface IAnswerRepositoryCustom<TEntity>
    {


    }
}