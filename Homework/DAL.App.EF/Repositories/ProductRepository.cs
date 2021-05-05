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

            query = query
                .Include(x => x.Condition)
                .ThenInclude(x => x!.Description)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Category)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.County)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Unit)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.City)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations);


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
                City = p.City!.Name,
                County = p.County!.Name,
                Category = p.Category!.Name,
                Unit = p.Unit!.Name,
                Condition = p.Condition!.Description,
                DateAdded = p.DateAdded,
                Material = p.ProductMaterials!.Select(x => x.Material!.Name!.ToString()),
                Height = p.Height,
                Width = p.Width,
                Depth = p.Depth,
                IsBooked = p.IsBooked,
                AppUserId = p.AppUserId

            }).OrderBy(p => p.DateAdded);

            return await resQuery.ToListAsync();
        }

        public async Task<DAL.App.DTO.Product?> FirstOrDefaultDTOAsync(Guid id)
        {
            var query = CreateQuery();

            var resQuery = query
                .Include(p => p.ProductMaterials)
                .ThenInclude(p => p.Material)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.City)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x.Translations)
                .Include(x => x.Condition)
                .ThenInclude(x => x!.Description)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Category)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.County)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(x => x.Unit)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Select(p => new DAL.App.DTO.Product()
            {
                Id = p.Id,
                Description = p.Description,
                County = p.County!.Name,
                CountyId = p.CountyId,
                City = p.City!.Name,
                CityId = p.CityId,
                Category = p.Category!.Name,
                CategoryId = p.CategoryId,
                Condition = p.Condition!.Description,
                ConditionId = p.ConditionId,
                Unit = p.Unit!.Name,
                UnitId = p.UnitId,
                Color = p.Color,
                Width = p.Width,
                Height = p.Height,
                Depth = p.Depth,
                AppUserId = p.AppUserId,
                DateAdded = p.DateAdded,
                IsBooked = p.IsBooked,
                HasTransport = p.HasTransport


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
                    .Include(x => x.City)
                    .ThenInclude(x => x!.Name)
                    .ThenInclude(x => x.Translations)
                    .Where(c => c.AppUserId == userId);
            }

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            return res!;
        }


        public override async Task<DAL.App.DTO.Product?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Include(c => c.County)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(p => p.Category)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(p => p.Unit)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x!.Translations)
                .Include(p => p.City)
                .ThenInclude(x => x!.Name)
                .ThenInclude(x => x.Translations);

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