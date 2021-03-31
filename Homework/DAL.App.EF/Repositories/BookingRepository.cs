using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DTO.App;
using Microsoft.EntityFrameworkCore;
using Booking = Domain.App.Booking;
using Product = Domain.App.Product;

namespace DAL.App.EF.Repositories
{
    public class BookingRepository  : BaseRepository<Booking, AppDbContext>,IBookingRepository
    {

        public BookingRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<BookingDTO>> GetAllDTOAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query.Select(booking => new BookingDTO()
            {
               ProductId = booking.ProductId,
               Until = booking.Until,
               TimeBooked = booking.TimeBooked,
               Product = new ProductDTO
               {
                   Description = booking.Product!.Description,
                   County = booking.Product.County!.Name,
                   Color = booking.Product.Color,
                   City = booking.Product.City!.Name
               }

            });

            var res = await resQuery.ToListAsync();

            return res;
        }

        public override async Task<Booking?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = CreateQuery();


            query = query
                .Include(p => p.Product);

            var res = await query.FirstOrDefaultAsync(m => m.ProductId == id && m.UserBookingId == userId);

            return res;
        }

    }
}