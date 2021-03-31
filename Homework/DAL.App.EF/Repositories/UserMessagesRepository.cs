using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DTO.App;
using Microsoft.EntityFrameworkCore;
using UserMessages = Domain.App.Identity.UserMessages;

namespace DAL.App.EF.Repositories
{
    public class UserMessagesRepository : BaseRepository<UserMessages, AppDbContext>,IUserMessagesRepository
    {
        public UserMessagesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<UserMessagesDTO>> GetAllMessagesAsync(string email)
        {
            var query = CreateQuery();

            var resQuery = query.Select(p => new UserMessagesDTO()
                {
                    Id = p.Id,
                    AppUserId = p.UserId,
                    MessageForm = new MessageForm
                    {
                        Message = p.MessageForm!.Message,
                        Email = p.MessageForm.Email,
                        Subject = p.MessageForm.Subject,
                        DateSent = p.MessageForm.DateSent,

                    }

                })
                .OrderByDescending(x => x.MessageForm!.DateSent);;

            return await resQuery.ToListAsync();

        }
    }
}