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

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var query = CreateQuery();


                query = query
                    .Include(c => c.City)
                    .Include(c => c.County)
                    .Include(c => c.Condition)
                    .Include(c => c.Category)
                    .Include(c => c.Unit);


            var res = await query.ToListAsync();


            return res;

        }


        public override async Task<IEnumerable<Product>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            if (userId != default)
            {
                query = query
                    .Where(c => c.AppUserId == userId)
                    .Include(c => c.City)
                    .Include(c => c.County)
                    .Include(c => c.Condition)
                    .Include(c => c.Category)
                    .Include(c => c.Unit);
            }

            var res = await query.ToListAsync();


            return res;

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

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);

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


    }

}