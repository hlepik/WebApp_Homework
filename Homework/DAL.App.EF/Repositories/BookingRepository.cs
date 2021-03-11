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
    public class BookingRepository : BaseRepository<Booking, AppDbContext>,IBookingRepository

    {
        public BookingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task DeleteAllBookingsByDateAsync(DateTime time)
        {

            foreach (var booking in await RepoDbSet.Where(x => x.Until == time).ToListAsync())
            {
                Remove(booking);
            }
        }
        public override async Task<IEnumerable<Booking>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<Booking?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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