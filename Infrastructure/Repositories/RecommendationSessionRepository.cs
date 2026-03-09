using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RecommendationSessionRepository(DTADbContext context) : Repository<RecommendationSession>(context), IRecommendationSessionRepository
    {
        public async Task<IReadOnlyCollection<RecommendationSession>> GetByUserIdAsync(int userId)
        {
            return await Entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
