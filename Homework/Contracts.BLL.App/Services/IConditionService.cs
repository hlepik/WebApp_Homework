using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace Contracts.BLL.App.Services
{
    public interface IConditionService : IBaseEntityService<Condition>, IConditionRepository
    {

    }
}