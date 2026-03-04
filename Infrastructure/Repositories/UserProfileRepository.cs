using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository(DTADbContext context) : Repository<UserProfile>(context), IUserProfileRepository
    {
    }
}
