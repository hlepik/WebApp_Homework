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
    public class MaterialRepository: BaseRepository<DAL.App.DTO.Material, Material, AppDbContext>,IMaterialRepository
    {
        public MaterialRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new MaterialMapper(mapper))
        {
        }

        public override async Task<DAL.App.DTO.Material?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}