using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class UserBookingsRepository : BaseRepository<UserBookings, AppDbContext>,IUserBookingsRepository
    {
        public UserBookingsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}