using System;
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

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}