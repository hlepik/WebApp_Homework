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
#pragma warning disable 1998
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
                .Include(x => x.Quiz);
            var resQuery = query
                .Select(p => new DAL.App.DTO.Question()
                {
                    Id = p.Id,
                    QuestionText = p.QuestionText,
                    QuizName = p.Quiz!.QuizName,
                    IsPoll = p.IsPoll,
                    MultipleChoice = p.MultipleChoice,
                    QuizId = p.QuizId


                }).OrderBy(x => x.QuizName);



            return await resQuery.ToListAsync();
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

            var query = CreateQuery();

            var resQuery = query
                .Include(p => p.Answers)
                .Select(p => new DAL.App.DTO.Question()
            {
                Id = p.Id,
                QuestionText = p.QuestionText,
                QuizName = p.Quiz!.QuizName,
                IsPoll = p.IsPoll,
                MultipleChoice = p.MultipleChoice,
                QuizId = p.QuizId,


            }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;

        }

        public  async Task<IEnumerable<DAL.App.DTO.Question>> GetAllWithIdAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Include(x => x.Answers)
                .Where(x => x.QuizId == id);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }


    }
}