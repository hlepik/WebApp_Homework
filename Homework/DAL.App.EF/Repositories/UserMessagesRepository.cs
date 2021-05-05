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

            var result = await query.FirstAsync();


            return result;
        }

        public async Task<IEnumerable<DAL.App.DTO.UserMessages>> GetAllMessagesAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            var resQuery = query
                .Include(p => p.MessageForm).Select(p => new DAL.App.DTO.UserMessages()
                {
                    Id = p.Id,
                    SenderEmail = p.SenderEmail,
                    Message = p.MessageForm!.Message,
                    Subject = p.MessageForm.Subject,
                    Email = p.MessageForm.Email,
                    AppUserId = p.AppUserId,
                    DateSent = p.MessageForm.DateSent


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
                    Subject = p.MessageForm!.Subject,
                    Message = p.MessageForm.Message,
                    SenderEmail = p.SenderEmail,
                    DateSent = p.MessageForm.DateSent


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

        public async Task<DTO.UserMessages> GetByMessageFormId(Guid id)
        {
            var query = CreateQuery();
            var res = await query.FirstOrDefaultAsync(m => m.MessageFormId == id);


            res.MessageFormId = new Guid();
            RepoDbContext.Update(res);

            return Mapper.Map(res)!;
        }


    }
}