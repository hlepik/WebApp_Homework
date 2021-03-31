using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using DTO.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository: BaseRepository<Product, AppDbContext>,IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetAllProductsIsNotBookedAsync()
        {
            var query = CreateQuery();


            query = query
                .Where(x => x.IsBooked == false);


            var res = await query.ToListAsync();


            return res;

        }


        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync(Guid? userId = default, bool noTracking = true)
        {
            var query = CreateQuery();

            var resQuery = query.Select(p => new ProductDTO()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Color = p.Color,
                    City = p.City!.Name,
                    DateAdded = p.DateAdded.Date,
                    LocationDescription = p.LocationDescription,
                    County = p.County!.Name
                })
                .OrderByDescending(x => x.DateAdded);;

            return await resQuery.ToListAsync();


        }

        public override async Task<Product?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.City)
                .Include(c => c.County)
                .Include(c => c.Condition)
                .Include(c => c.Category)
                .Include(c => c.Unit);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return res;
        }
        public async Task<Product> FirstOrDefaultWithoutOutIdAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Include(p => p.City)
                .Include(c => c.County)
                .Include(c => c.Condition)
                .Include(c => c.Category)
                .Include(c => c.Unit);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return res;
        }


        public async Task<Product> ChangeBookingStatus(Guid id)
        {

            var query = CreateQuery();
            var product = await query.FirstOrDefaultAsync(m => m.Id == id);

            return product;
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Where(c => c.AppUserId == userId)
                .Include(c => c.City)
                .Include(c => c.County);

            var res = await query.ToListAsync();


            return res;
        }


    }

}