using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class ConditionRepository: BaseRepository<Condition, AppDbContext>,IConditionRepository
    {
        public ConditionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}