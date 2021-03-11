using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MaterialRepository :  BaseRepository<Material, AppDbContext>,IMaterialRepository
    {
        public MaterialRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllMaterialByDateAsync(Guid id)
        {

            foreach (var material in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(material);
            }
        }

        public override async Task<IEnumerable<Material>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Name);
            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<Material?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return res;
        }
    }
}