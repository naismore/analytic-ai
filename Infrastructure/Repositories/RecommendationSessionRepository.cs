using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;

namespace Infrastructure.Repositories
{
    public class RecommendationSessionRepository(DTADbContext context) : Repository<RecommendationSession>(context), IRecommendationSessionRepository
    {
    }
}
