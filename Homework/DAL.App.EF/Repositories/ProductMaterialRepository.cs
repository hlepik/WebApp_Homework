using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DTO.App;
using Microsoft.EntityFrameworkCore;
using ProductMaterial = Domain.App.ProductMaterial;

namespace DAL.App.EF.Repositories
{
    public class ProductMaterialRepository : BaseRepository<ProductMaterial, AppDbContext>,IProductMaterialRepository
    {
        public ProductMaterialRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<ProductMaterial>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(p => p.Products)
                .Include(c => c.Material)
                .Where(c => c.Products!.AppUserId == userId);



            var res = await query.ToListAsync();


            return res;

        }

        public override async Task<ProductMaterial?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Include(p => p.Products)
                .Include(c => c.Material);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.Products!.AppUserId == userId);

            return res;
        }

        public async Task<IEnumerable<ProductMaterialDTO>> GetAllProductMaterialsAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery();

            var resQuery = query.Select(p => new ProductMaterialDTO()
                {
                    Id = p.Id,
                    Products = new ProductDTO
                    {
                        AppUserId = p.Products!.AppUserId,
                        Description = p.Products!.Description,
                    },
                    ProductId = p.ProductId,
                    MaterialId = p.MaterialId,
                    Material = new Material
                    {
                        Name = p.Material!.Name
                    }
                })
                .OrderByDescending(x => x.Products!.Description)
                .Where(c => c.Products!.AppUserId == userId);

            return await resQuery.ToListAsync();
        }
    }
}