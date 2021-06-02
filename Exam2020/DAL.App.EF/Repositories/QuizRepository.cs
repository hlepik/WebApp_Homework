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
using Answer = Domain.App.Answer;

namespace DAL.App.EF.Repositories
{

    public class QuizRepository: BaseRepository<DTO.Quiz, Domain.App.Quiz, AppDbContext>,
        IQuizRepository
    {
        public QuizRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new QuizMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Quiz>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(x => x.Questions)
                .OrderBy(x => x.CreatedAt);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }

        public async Task<string?> GetName(Guid id)
        {
            var query = CreateQuery();

            string? res = query
                .Where(x => x.Id == id).Select(x => x.QuizName).First();

            return res;
        }

        public async Task<IEnumerable<DAL.App.DTO.Quiz>> GetSearchResult(string searchName)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.QuizName.Contains(searchName)).OrderBy(x => x.CreatedAt);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;
        }

        public override async Task<Quiz?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.Questions)
                .ThenInclude(t => t.Answers);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);


            return Mapper.Map(res);
        }
        public  async Task<int> GetQuestionsCount(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Include(c => c.Questions);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);


            return res.Questions!.Count;
        }
        public  async Task<int> GetCorrectAnswers(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Include(c => c.Questions)
                .ThenInclude(c => c.Answers)
                .Where(x => x.Id == id);

            var count = 0;
            foreach (var quiz in query)
            {
                foreach (var question in quiz.Questions!)
                {
                    foreach (var answer in question.Answers!)
                    {
                        if (answer.IsAnswerCorrect && !question.IsPoll)
                        {
                            count++;
                        }
                    }

                }
            }


            return count;
        }
    }
}