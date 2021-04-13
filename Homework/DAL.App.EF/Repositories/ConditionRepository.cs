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
    public class ConditionRepository: BaseRepository<DTO.Condition, Domain.App.Condition, AppDbContext>,IConditionRepository
    {
        public ConditionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ConditionMapper(mapper))
        {
        }

        public override async Task<DTO.Condition?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
        public override async Task<IEnumerable<DAL.App.DTO.Condition>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .OrderBy(x => x.Description);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }
    }
}