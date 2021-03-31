using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;

namespace BLL.App.Services
{
    public class ConditionService: BaseEntityService<IAppUnitOfWork, IConditionRepository, Condition>, IConditionService
    {
        public ConditionService(IAppUnitOfWork serviceUow, IConditionRepository serviceRepository) : base(serviceUow, serviceRepository)
        {
        }

    }
}