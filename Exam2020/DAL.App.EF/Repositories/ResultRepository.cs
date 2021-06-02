
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
    public class ResultRepository: BaseRepository<DTO.Result, Domain.App.Result, AppDbContext>, IResultRepository
    {
        public ResultRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ResultMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Result>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(x => x.Quiz)
                .OrderBy(x => x.Quiz!.QuizName);


            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }
    }
}