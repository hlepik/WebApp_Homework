using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO.Identity;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using AppUser = Domain.App.Identity.AppUser;

namespace DAL.App.EF.Repositories
{
    public class UserMessagesRepository : BaseRepository<DAL.App.DTO.UserMessages, Domain.App.UserMessages, AppDbContext>,IUserMessagesRepository
    {
        public UserMessagesRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new UserMessagesMapper(mapper))
        {
        }


        public async Task<Guid?> GetId(string email)
        {
            var query = RepoDbContext
                .Users.Where(x => x.Email == email)
                .Select(x => x.Id );

            if (!query.Any())
            {
                return Guid.Empty;
            }
            var result = await query.FirstAsync();


            return result;
        }

        public async Task<IEnumerable<DAL.App.DTO.UserMessages>> GetAllMessagesAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            var resQuery = query
                .Select(p => new DAL.App.DTO.UserMessages()
                {
                    Id = p.Id,
                    SenderEmail = p.SenderEmail,
                    Subject = p.Subject,
                    Message = p.Message,
                    AppUserId = p.AppUserId,
                    DateSent = p.DateSent

                }).OrderBy(x => x.DateSent).Where(p => p.AppUserId == userId);

            return await resQuery.ToListAsync();

        }
        public async Task<DAL.App.DTO.UserMessages?> FirstOrDefaultUserMessagesAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            var resQuery = query
                .Select(p => new DAL.App.DTO.UserMessages()
                {
                    Id = p.Id,
                    AppUserId = p.AppUserId,
                    Subject = p.Subject,
                    Message = p.Message,
                    DateSent = p.DateSent,
                    SenderEmail = p.SenderEmail,



                }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;
        }

        public void RemoveUserMessagesAsync(Guid id, Guid userId)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.Id == id && x.AppUserId == userId);

            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }
        }

        public void RemoveUserMessagesByUser(Guid userId)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.AppUserId == userId);

            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }
        }




    }
}