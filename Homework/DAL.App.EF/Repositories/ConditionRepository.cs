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
    public class ConditionRepository :  BaseRepository<Condition, AppDbContext>,IConditionRepository
    {
        public ConditionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllConditionByDateAsync(Guid id)
        {

            foreach (var condition in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(condition);
            }
        }

        public override async Task<IEnumerable<Condition>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Description);
            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<Condition?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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