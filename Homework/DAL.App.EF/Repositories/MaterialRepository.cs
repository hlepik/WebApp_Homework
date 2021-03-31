using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class MaterialRepository: BaseRepository<Material, AppDbContext>,IMaterialRepository
    {
        public MaterialRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}