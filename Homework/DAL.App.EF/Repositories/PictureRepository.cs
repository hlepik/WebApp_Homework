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
    public class PictureRepository : BaseRepository<Picture, AppDbContext>,IPictureRepository
    {

        public PictureRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Picture>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Product)
                .Where(c => c.Product!.AppUserId == userId);;



            var res = await query.ToListAsync();


            return res;

        }

        public override async Task<Picture?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Include(p => p.Product);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.Product!.AppUserId == userId);

            return res;
        }
    }

}