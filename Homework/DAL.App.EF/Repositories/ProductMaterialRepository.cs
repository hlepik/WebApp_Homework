using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DTO.App;
using Microsoft.EntityFrameworkCore;
using ProductMaterial = Domain.App.ProductMaterial;


namespace DAL.App.EF.Repositories
{
    public class ProductMaterialRepository : BaseRepository<DAL.App.DTO.ProductMaterial, ProductMaterial, AppDbContext>,IProductMaterialRepository
    {
        public ProductMaterialRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProductMaterialMapper(mapper))
        {
        }


        public async Task<DAL.App.DTO.ProductMaterial> FirstOrDefaultDTOAsync(Guid id = default, Guid userId = default,
            bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);

            var resQuery = query.Include(p => p.Material)
                .Include(p => p.Products)
                .Select(p => new DAL.App.DTO.ProductMaterial()
            {
                Id = p.Id,
                ProductName = p.Products!.Description,
                MaterialName = p.Material!.Name,
                ProductOwner = p.Products.AppUserId,
                ProductId = p.ProductId,
                MaterialId = p.MaterialId

            }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;
        }

        public async Task<IEnumerable<DAL.App.DTO.ProductMaterial>> GetAllProductMaterialsAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query.Include(x => x.Products).Include(x => x.Material);

            var resQuery = query.Select(p => new DAL.App.DTO.ProductMaterial()
                {
                    Id = p.Id,
                    ProductName = p.Products!.Description,
                    MaterialName = p.Material!.Name,
                    ProductOwner = p.Products.AppUserId,
                    ProductId = p.ProductId,
                    MaterialId = p.MaterialId

                }).Where(p => p.ProductOwner == userId)
                .OrderBy(x => x.ProductName);

            return await resQuery.ToListAsync();
        }





    }
}