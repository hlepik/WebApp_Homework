using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.Domain.Base;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserBookedProductsRepository  : BaseRepository<UserBookedProducts, AppDbContext>,IUserBookedProductsRepository
    {

        public UserBookedProductsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<UserBookedProducts>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            if (userId != default)
            {
                query = query
                    .Where(c => c.AppUserId == userId)
                    .Include(c => c.Product);

            }

            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<UserBookedProducts?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Include(p => p.Product);



            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);

            return res;
        }
    }

}
