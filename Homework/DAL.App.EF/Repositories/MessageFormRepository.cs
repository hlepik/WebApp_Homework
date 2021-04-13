using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MessageFormRepository: BaseRepository<DAL.App.DTO.MessageForm, MessageForm, AppDbContext>,IMessageFormRepository
    {
        public MessageFormRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new MessageFormMapper(mapper))
        {
        }



        public override async Task<IEnumerable<DAL.App.DTO.MessageForm>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Where(x => x.SenderId == userId).OrderBy(x => x.DateSent);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            return res!;

        }


        public async Task<DAL.App.DTO.MessageForm?> FirstOrDefaultMessagesAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(default, noTracking);


            var resQuery = query.Select(p => new DAL.App.DTO.MessageForm()
            {
                Id = p.Id,
                Email = p.Email,
                Message = p.Message,
                Subject = p.Subject,
                DateSent = p.DateSent,
                SenderId = p.SenderId


            }).FirstOrDefaultAsync(m => m.Id == id);

            return await resQuery;

        }
        public void RemoveMessagesAsync(Guid id)
        {
            var query = CreateQuery();

            query = query
                .Where(x => x.Id == id);

            foreach (var l in query)
            {
                RepoDbSet.Remove(l);
            }
        }
    }


}