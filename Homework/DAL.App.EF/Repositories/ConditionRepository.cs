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
    public class ConditionRepository: BaseRepository<DTO.Condition, Domain.App.Condition, AppDbContext>,IConditionRepository
    {
        public ConditionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ConditionMapper(mapper))
        {
        }

        public override Condition Update(Condition condition)
        {
            var domainEntity = Mapper.Map(condition);
            domainEntity!.Description = RepoDbContext.LangStrings
                .Include(x => x.Translations)
                .First(x => x.Id == domainEntity.DescriptionId);
            domainEntity!.Description.SetTranslation(condition.Description);

            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;

        }

        public override async Task<DTO.Condition?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.Description)
                .ThenInclude(t => t!.Translations);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
        public override async Task<IEnumerable<DAL.App.DTO.Condition>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(x => x.Description)
                .ThenInclude(x => x!.Translations)
                .OrderBy(x => x.Description);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }
    }
}