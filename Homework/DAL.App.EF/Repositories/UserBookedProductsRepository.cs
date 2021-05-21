using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
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


            var resQuery = query
                .Include(p => p.Product)
                .ThenInclude(p => p!.Booking)
                .Select(p => new DAL.App.DTO.UserBookedProducts()
                {
                    Id = p.Id,
                    Description = p.Product!.Description,
                    AppUserId = p.AppUserId,
                    Email = p.Product.AppUser!.Email,
                    TimeBooked = p.Product.Booking!.Select(p => p.TimeBooked).FirstOrDefault(),
                    Until = p.Product.Booking.Select(p => p.Until).FirstOrDefault(),
                    ProductId = p.ProductId


                }).Where(x => x.AppUserId == userId);

            return await resQuery.ToListAsync();
        }

        public async Task<DAL.App.DTO.UserBookedProducts?> FirstOrDefaultBookedProductsAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);


            query = query
                .Include(p => p.Product);


            var resQuery = query
                .Select(p => new DAL.App.DTO.UserBookedProducts()
                {

                    Id = p.Id,
                    ProductId = p.ProductId


                }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;
        }


        public async Task<Guid> GetId(Guid id)
        {
            var query = RepoDbContext
                .UserBookedProducts
                .Include(x => x.Product)
                .Where(x => x.Id == id)
                .Select(x => x.ProductId);

            return await query.FirstAsync();
        }

        public void RemoveUserBookedProductsAsync(Guid? id, Guid userId = default)
        {
            var query = CreateQuery(userId);

            if (id != null)
            {
                query = query
                    .Where(x => x.ProductId == id);
            }

            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }

        }
    }

}
