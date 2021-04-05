using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using UserBookedProducts = Domain.App.UserBookedProducts;

namespace DAL.App.EF.Repositories
{
    public class UserBookedProductsRepository  : BaseRepository<DAL.App.DTO.UserBookedProducts, UserBookedProducts, AppDbContext>,IUserBookedProductsRepository
    {

        public UserBookedProductsRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new UserBookedProductsMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.UserBookedProducts>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            if (userId != default)
            {
                query = query
                    .Include(c => c.Booking!.Product)
                    .Where(c => c.Booking!.AppUserId == userId);
            }

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }

        public async Task<DAL.App.DTO.UserBookedProducts?> FirstOrDefaultBookedProductsAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);


            query = query
                .Include(p => p.Booking);


            var resQuery = query
                .Select(p => new DAL.App.DTO.UserBookedProducts()
                {
                    Id = p.Id,
                    Description = p.Booking!.Product!.Description,
                    TimeBooked = p.Booking.TimeBooked,
                    Until = p.Booking!.Until,
                    City = p.Booking.Product.City!.Name,
                    Color = p.Booking.Product.Color,
                    County = p.Booking.Product.County!.Name,
                    LocationDescription = p.Booking.Product.LocationDescription


                }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;
        }

        public async Task<IEnumerable<DAL.App.DTO.UserBookedProducts>> GetAllBookedProductsAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query.Select(p => new DAL.App.DTO.UserBookedProducts()
                {
                    Id = p.Id,
                    Description = p.Booking!.Product!.Description,
                    TimeBooked = p.Booking.TimeBooked,
                    AppUserId = p.Booking.AppUserId,
                    BookingId = p.BookingId

                })
                .Where(x => x.AppUserId == userId).OrderByDescending(x => x.TimeBooked);

            return await resQuery.ToListAsync();


        }
    }

}
