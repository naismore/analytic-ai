using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecommendationSessionRepository : IRepository<RecommendationSession>
    {
        Task<IReadOnlyCollection<RecommendationSession>> GetByUserIdAsync(int userId);
    }
}
