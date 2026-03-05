using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        Task<UserProfile?> GetByUserIdAsync(int userId);
    }
}
