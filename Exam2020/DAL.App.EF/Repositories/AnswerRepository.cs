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
                .Include(x => x.Question);

            var resQuery = query
                .Select(p => new DAL.App.DTO.Answer()
                {
                    Id = p.Id,
                    QuestionAnswer = p.QuestionAnswer,
                    IsAnswerCorrect = p.IsAnswerCorrect,
                    QuestionName = p.Question!.QuestionText,
                    QuestionId = p.QuestionId,


                }).OrderBy(x => x.QuestionName);
            return await resQuery.ToListAsync();
        }
        public async Task<IEnumerable<DAL.App.DTO.Answer>> GetAllAnswersAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Include(x => x.Question)
                .Where(x => x.QuestionId == id);

            var resQuery = query
                .Select(p => new DAL.App.DTO.Answer()
                {
                    Id = p.Id,
                    QuestionAnswer = p.QuestionAnswer,
                    IsAnswerCorrect = p.IsAnswerCorrect,
                    QuestionName = p.Question!.QuestionText,
                    QuestionId = p.QuestionId,


                }).OrderBy(x => x.QuestionName);
            return await resQuery.ToListAsync();
        }

        public void RemoveAnswerAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.QuestionId == id);
            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }

        }


    }
}