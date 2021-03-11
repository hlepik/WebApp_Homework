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
    public class BookingStatusRepository : BaseRepository<BookingStatus, AppDbContext>,IBookingStatusRepository
    {
        public BookingStatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllBookingStatusByDateAsync(Guid id)
        {

            foreach (var bookingStatus in await RepoDbSet.Where(x => x.BookingId == id).ToListAsync())
            {
                Remove(bookingStatus);
            }
        }

        public override async Task<IEnumerable<BookingStatus>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Booking);
            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<BookingStatus?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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