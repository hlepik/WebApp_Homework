using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Domain.App.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MessageFormRepository: BaseRepository<MessageForm, AppDbContext>,IMessageFormRepository
    {
        public MessageFormRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public override async Task<IEnumerable<MessageForm>> GetAllAsync(Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Where(x => x.SenderId == userId);

            var res = await query.ToListAsync();
            return res;

        }
        public async Task<IEnumerable<MessageForm>> GetAllMessagesAsync(string email)
        {
            var query = CreateQuery();


            query = query

                .Where(x => x.Email == email);

            var res = await query.ToListAsync();
            return res;

        }

        public override async Task<MessageForm?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);


            query = query
                .Include(p => p.UserMessages)
                .ThenInclude(c => c.AppUser);

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return res;
        }
    }


}