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
    public class QuestionRepository : BaseRepository<DTO.Question, Domain.App.Question, AppDbContext>,
        IQuestionRepository
    {

        public QuestionRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuestionMapper(mapper))
        {
        }
        public override async Task<IEnumerable<DAL.App.DTO.Question>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(x => x.Quiz)
                .OrderBy(x => x.Quiz!.QuizName);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }
        public async Task<string?> GetName(Guid id)
        {
            var query = CreateQuery();

            string? res = query
                .Where(x => x.Id == id).Select(x => x.QuestionText).First();

            return res;
        }
        public override async Task<Question?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.Answers);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);


            return Mapper.Map(res);
        }

    }
}