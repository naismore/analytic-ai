using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;
using System.Data.Entity;

namespace Infrastructure.Repositories
{
    public class RecommendationResultRepository(DTADbContext context) : Repository<RecommendationResult>(context), IRecommendationResultRepository
    {
    }
}
