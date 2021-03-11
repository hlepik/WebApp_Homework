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
    public class CountyRepository :  BaseRepository<County, AppDbContext>,ICountyRepository
    {
        public CountyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllCountyByDateAsync(Guid id)
        {

            foreach (var county in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(county);
            }
        }

        public override async Task<IEnumerable<County>> GetAllAsync(bool noTracking = true)
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

        public override async Task<County?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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