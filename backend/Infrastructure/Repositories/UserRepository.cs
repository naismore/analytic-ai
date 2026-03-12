using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;

namespace Infrastructure.Repositories
{
    public class UserRepository(DTADbContext context) : Repository<User>(context), IUserRepository
    {
    }
}
