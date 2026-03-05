using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository(DTADbContext context) : Repository<UserProfile>(context), IUserProfileRepository
    {
        public async Task<UserProfile?> GetByUserIdAsync(int userId)
        {
            return await base.Entities.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
