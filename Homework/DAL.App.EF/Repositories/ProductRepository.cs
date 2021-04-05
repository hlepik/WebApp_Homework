using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
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
            var query = CreateQuery(default, noTracking);

            query = query.Include(x => x.City)
                .Include(x => x.County)
                .Include(x => x.Category)
                .Include(x => x.Unit);

            var resQuery = query
                .Select(p => new DAL.App.DTO.Product()
            {
                Id = p.Id,
                Description = p.Description,
                Height = p.Height,
                Depth = p.Depth,
                Width = p.Width,
                LocationDescription = p.LocationDescription,
                IsBooked = p.IsBooked,
                HasTransport = p.HasTransport,
                Color = p.Color,
                DateAdded = p.DateAdded,
                CityName = p.City!.Name,
                CityId = p.CityId,
                CountyName = p.County!.Name,
                CountyId = p.CountyId,
                CategoryName = p.Category!.Name,
                CategoryId = p.CategoryId,
                UnitName = p.Unit!.Name,
                UnitId = p.UnitId,
                ConditionName = p.Condition!.Description,
                ConditionId = p.ConditionId

            }).OrderBy(p => p.DateAdded);

            return await resQuery.ToListAsync();
        }

        public async Task<DAL.App.DTO.Product> FirstOrDefaultDTOAsync(Guid id)
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
        public async Task<DAL.App.DTO.Product> FirstOrDefaultWithoutOutIdAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Include(p => p.City)
                .Include(c => c.County)
                .Include(c => c.Condition)
                .Include(c => c.Category)
                .Include(c => c.Unit);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res)!;
        }


        public async Task<DAL.App.DTO.Product> ChangeBookingStatus(Guid id)
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
                    .Where(c => c.AppUserId == userId);
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


    }

}