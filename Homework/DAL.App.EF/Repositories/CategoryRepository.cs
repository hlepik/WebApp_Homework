using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
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
        public override Category Update(Category category)
        {
            var domainEntity = Mapper.Map(category);
            domainEntity!.Name = RepoDbContext.LangStrings
                .Include(x => x.Translations)
                .First(x => x.Id == domainEntity.NameId);
            domainEntity!.Name.SetTranslation(category.Name);

            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;

        }

        public override async Task<IEnumerable<DAL.App.DTO.Category>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations)
                .OrderBy(x => x.Name);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }

        public override async Task<DAL.App.DTO.Category?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}