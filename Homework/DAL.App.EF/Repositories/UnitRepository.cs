using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class UnitRepository: BaseRepository<Unit, AppDbContext>,IUnitRepository
    {
        public UnitRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}