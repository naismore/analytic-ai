using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RecommendationResultRepository(DTADbContext context) : Repository<RecommendationResult>(context), IRecommendationResultRepository
    {
        public async Task<IReadOnlyCollection<RecommendationResult>> GetBySessionIdAsync(Guid sessionId)
        {
            return await Entities.Where(x => x.SessionId == sessionId).ToListAsync();
        }
    }
}
