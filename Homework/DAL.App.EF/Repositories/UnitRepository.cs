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
    public class UnitRepository: BaseRepository<DAL.App.DTO.Unit, Unit, AppDbContext>,IUnitRepository
    {
        public UnitRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new UnitMapper(mapper))
        {
        }

        public override async Task<DAL.App.DTO.Unit?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
        public override async Task<IEnumerable<DAL.App.DTO.Unit>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .OrderBy(x => x.Name);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }
    }
}