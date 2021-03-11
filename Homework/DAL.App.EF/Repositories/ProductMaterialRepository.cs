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
    public class ProductMaterialRepository :  BaseRepository<ProductMaterial, AppDbContext>,IProductMaterialRepository
    {
        public ProductMaterialRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task DeleteAllProductMaterialsByDateAsync(Guid id)
        {

            foreach (var productMaterial in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(productMaterial);
            }
        }

        public override async Task<IEnumerable<ProductMaterial>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Products)
                .Include(p => p.Material);
            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<ProductMaterial?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
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