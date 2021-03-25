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
    public class BookingRepository  : BaseRepository<Booking, AppDbContext>,IBookingRepository
    {

        public BookingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<Booking>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Product)
                .Where(c => c.Product!.IsBooked == false);



            var res = await query.ToListAsync();

            return res;
        }

        public override async Task<Booking?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery();


            query = query
                .Include(p => p.Product);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.Product!.AppUserId == userId);

            return res;
        }


    }
}