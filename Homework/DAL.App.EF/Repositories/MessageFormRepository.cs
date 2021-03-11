using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class MessageFormRepository : BaseRepository<MessageForm, AppDbContext>,IMessageFormRepository
    {
        public MessageFormRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task DeleteAllMessageFormsByDateAsync(Guid id)
        {

            foreach (var messageForm in await RepoDbSet.Where(x => x.Id == id).ToListAsync())
            {
                Remove(messageForm);
            }
        }

        public override async Task<IEnumerable<MessageForm>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.Email)
                .Include(p => p.Message);
            var res = await query.ToListAsync();


            return res;
        }

        public override async Task<MessageForm?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return res;
        }
    }
}