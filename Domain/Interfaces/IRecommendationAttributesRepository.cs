using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRecommendationAttributesRepository : IRepository<RecommendationAttributes>
    {
        IReadOnlyCollection<Guid> GetBySessionIdsAsync(IEnumerable<Guid> sessionIds);
        Task<IReadOnlyCollection<RecommendationAttributes>> GetByIdsAsync(IEnumerable<Guid> ids);
    }
}
