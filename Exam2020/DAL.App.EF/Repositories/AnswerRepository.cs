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

    public class AnswerRepository : BaseRepository<Answer, Domain.App.Answer, AppDbContext>,IAnswerRepository
    {

        public AnswerRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new AnswerMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Answer>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(x => x.Question)
                .OrderBy(x => x.Question);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }


    }
}