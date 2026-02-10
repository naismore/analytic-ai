using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RecommendationResultRepository(DTADbContext context) : IRecommendationResultRepository
    {
        public async Task<IReadOnlyCollection<RecommendationResult>> GetListAsync() =>
            await context.RecommendationResults.ToListAsync();
        public async Task<RecommendationResult> GetByIdAsync(int id) =>
            await context.RecommendationResults.FindAsync(id);
        public async Task InsertAsync(RecommendationResult recommendationResult) =>
            await context.RecommendationResults.AddAsync(recommendationResult);
        public void Update(RecommendationResult recommendationResult) =>
            context.RecommendationResults.Update(recommendationResult);
        public void Delete(RecommendationResult recommendationResult) =>
            context.RecommendationResults.Remove(recommendationResult);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
