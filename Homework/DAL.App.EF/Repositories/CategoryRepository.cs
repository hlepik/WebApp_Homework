using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CategoryRepository : BaseRepository<DAL.App.DTO.Category, Domain.App.Category, AppDbContext>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CategoryMapper(mapper))
        {
        }

        public  async Task<IEnumerable<DAL.App.DTO.Category>> GetAllCategoriesAsync()
        {
            var query = CreateQuery();

            var resQuery = query.Select(p => new DAL.App.DTO.Category()
                {
                    Name = p.Name,
                    Id = p.Id
                })
                .OrderBy(x => x.Name);

            return await resQuery.ToListAsync();

        }

        public override async Task<DAL.App.DTO.Category?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}