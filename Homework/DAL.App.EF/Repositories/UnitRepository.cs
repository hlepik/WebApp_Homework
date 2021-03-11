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
    public class UnitRepository :  BaseRepository<Unit, AppDbContext>,IUnitRepository
    {
        public UnitRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task DeleteAllUnitsByDateAsync(Guid id)
        {

            foreach (var unit in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(unit);
            }
        }

        public override async Task<IEnumerable<Unit>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }


            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<Unit?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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