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
    public class ProductRepository : BaseRepository<Product, AppDbContext>,IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllProductsByDateAsync(Guid id)
        {

            foreach (var product in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(product);
            }
        }

        public override async Task<IEnumerable<Product>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Category)
                .Include(p => p.Condition)
                .Include(p => p.Unit)
                .Include(p => p.County);
            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<Product?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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