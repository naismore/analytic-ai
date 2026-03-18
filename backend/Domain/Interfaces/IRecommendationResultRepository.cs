using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecommendationResultRepository : IRepository<RecommendationResult>
    {
        Task<IReadOnlyCollection<RecommendationResult>> GetBySessionIdAsync(Guid sessionId);
    }
}
