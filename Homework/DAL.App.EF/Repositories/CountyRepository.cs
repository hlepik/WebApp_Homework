using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class CountyRepository: BaseRepository<County, AppDbContext>,ICountyRepository
    {
        public CountyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}