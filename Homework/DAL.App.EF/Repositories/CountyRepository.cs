using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CountyRepository: BaseRepository<DAL.App.DTO.County, County, AppDbContext>,ICountyRepository
    {
        public CountyRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CountyMapper(mapper))
        {
        }


        public override async Task<DAL.App.DTO.County?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.Name)
                .ThenInclude(t => t!.Translations);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
        public override async Task<IEnumerable<DAL.App.DTO.County>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var resQuery = query
                .Include(x => x.Name)
                .ThenInclude(x => x!.Translations)
                .OrderBy(x => x.Name);

            var resDomain = await resQuery.ToListAsync();
            var res = resDomain.Select(x => Mapper.Map(x));
            return res!;
        }

        public override DTO.County Update(DTO.County county)
        {

            var domainEntity = Mapper.Map(county);
            domainEntity!.Name = RepoDbContext.LangStrings
                .Include(x => x.Translations)
                .First(x => x.Id == domainEntity.NameId);
            domainEntity!.Name.SetTranslation(county.Name);

            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;

        }
    }
}