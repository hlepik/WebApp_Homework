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
    public class PictureRepository :  BaseRepository<Picture, AppDbContext>,IPictureRepository
    {
        public PictureRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllPicturesByDateAsync(Guid id)
        {

            foreach (var picture in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(picture);
            }
        }

        public override async Task<IEnumerable<Picture>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Product);


            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<Picture?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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