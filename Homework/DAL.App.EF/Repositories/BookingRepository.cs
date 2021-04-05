using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class BookingRepository  : BaseRepository<DAL.App.DTO.Booking, Domain.App.Booking, AppDbContext>,IBookingRepository
    {

        public BookingRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new BookingMapper(mapper))
        {
        }


        public async Task<Booking> FirstOrDefaultDTOAsync(Guid id, Guid userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);


            query = query
                .Include(p => p.Product);

            if (userId != default)
            {
                query = query.Where(p => p.AppUserId == userId);
            }

            var resQuery = query.Select(p => new DAL.App.DTO.Booking()
            {
                Id = p.Id,
                ProductName = p.Product!.Description,
                TimeBooked = p.TimeBooked,
                ProductId = p.ProductId,
                AppUserId = p.AppUserId,
                City = p.Product.City!.Name,
                County = p.Product.County!.Name,
                LocationDescription = p.Product.LocationDescription,
                Width = p.Product.Width,
                Height = p.Product.Height,
                Depth = p.Product.Depth,
                Unit = p.Product.Unit!.Name


            }).FirstOrDefaultAsync(m => m.ProductId == id);

            return await resQuery;
        }

    }
}