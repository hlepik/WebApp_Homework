using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Product = Domain.App.Product;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository: BaseRepository<DAL.App.DTO.Product, Product, AppDbContext>,IProductRepository
    {

        public ProductRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProductMapper(mapper))
        {

        }

        public async Task<IEnumerable<DAL.App.DTO.Product>> GetAllProductsIsNotBookedAsync()
        {
            var query = CreateQuery();


            query = query
                .Where(x => x.IsBooked == false);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            return res!;

        }

        public async Task<IEnumerable<DAL.App.DTO.Product>> GetAllProductsAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query.Include(x => x.City)
                .Include(x => x.County)
                .Include(x => x.Category)
                .Include(x => x.Unit)
                .Include(p => p.ProductMaterials);


            if (userId != default)
            {
                query = query.Where(x => x.AppUserId == userId);
            }
            var resQuery = query
                .Select(p => new DAL.App.DTO.Product()
            {
                Id = p.Id,
                Description = p.Description,
                Color = p.Color,
                CityName = p.City!.Name,
                CountyName = p.County!.Name,
                CategoryName = p.Category!.Name,
                UnitName = p.Unit!.Name,
                ConditionName = p.Condition!.Description,
                DateAdded = p.DateAdded,
                Material = p.ProductMaterials!.Select(x => x.Material!.Name),
                Height = p.Height,
                Width = p.Width,
                Depth = p.Depth

            }).OrderBy(p => p.DateAdded);

            return await resQuery.ToListAsync();
        }

        public async Task<DAL.App.DTO.Product?> FirstOrDefaultDTOAsync(Guid id)
        {
            var query = CreateQuery();

            var resQuery = query
                .Include(p => p.ProductMaterials)
                .ThenInclude(p => p.Material)
                .Select(p => new DAL.App.DTO.Product()
            {
                Id = p.Id,
                Description = p.Description,
                CountyName = p.County!.Name,
                CountyId = p.CountyId,
                CityName = p.City!.Name,
                CityId = p.CityId,
                CategoryName = p.Category!.Name,
                CategoryId = p.CategoryId,
                ConditionName = p.Condition!.Description,
                ConditionId = p.ConditionId,
                UnitName = p.Unit!.Name,
                UnitId = p.UnitId,

            }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;

        }

        public async Task<DTO.Product> ChangeBookingStatus(Guid? id)
        {

            var query = CreateQuery();
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res)!;
        }

        public override async Task<IEnumerable<DAL.App.DTO.Product>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {

            var query = CreateQuery(userId, noTracking);

            if (userId != default)
            {
                query = query
                    .Include(x => x.City).Where(c => c.AppUserId == userId);
            }

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            return res!;
        }


        public override async Task<DAL.App.DTO.Product?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Include(p => p.City)
                .Include(c => c.County)
                .Include(p => p.Category)
                .Include(p => p.Unit);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }

        public void RemoveProductAsync(Guid id, Guid userId = default)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.Id == id);

            foreach (var l in query)
            {

                RepoDbSet.Remove(l);
            }
        }

        public async Task<IEnumerable<DAL.App.DTO.Product?>> GetId(Guid userId)
        {
            var query = CreateQuery();
            var resQuery= query
                .Where(c => c.AppUserId == userId);

            var res = await resQuery.Select(x => Mapper.Map(x)).ToListAsync();

            return res!;
        }


        public void DeleteAll(Guid userId)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.AppUserId == userId);

            foreach (var l in query)
            {

                RepoDbSet.Remove(l);
            }


        }




    }

}