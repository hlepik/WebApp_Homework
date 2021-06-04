
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
                .Where(x => x.AppUserId == userId);
            var resQuery = query
                .Select(p => new DAL.App.DTO.Result()
                {
                    Id = p.Id,
                    Percentage = p.Percentage,
                    QuizName = p.Quiz!.QuizName,
                    CorrectAnswersCount = p.CorrectAnswersCount

                }).OrderBy(x => x.QuizName);



            return await resQuery.ToListAsync();
        }
        public void RemoveResultAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.QuizId == id);
            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }

        }
    }
}