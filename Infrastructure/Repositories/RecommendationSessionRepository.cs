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
    public class RecommendationSessionRepository(DTADbContext context) : IRecommendationSessionRepository
    {
        public async Task<IReadOnlyCollection<RecommendationSession>> GetListAsync() =>
            await context.RecommendationSessions.ToListAsync();
        public async Task<RecommendationSession> GetByIdAsync(int id) =>
            await context.RecommendationSessions.FindAsync(id);
        public async Task InsertAsync(RecommendationSession recommendationSession) =>
            await context.RecommendationSessions.AddAsync(recommendationSession);
        public void Update(RecommendationSession recommendationSession) =>
            context.RecommendationSessions.Update(recommendationSession);
        public void Delete(RecommendationSession recommendationSession) =>
            context.RecommendationSessions.Remove(recommendationSession);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
