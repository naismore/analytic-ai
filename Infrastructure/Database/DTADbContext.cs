using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DTADbContext(DbContextOptions<DTADbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<AnalyticsTool> AnalyticsTools { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<RecommendationSession> RecommendationSessions { get; set; } = null!;
        public DbSet<RecommendationResult> RecommendationResults { get; set; } = null!;
        public DbSet<RecommendationAttributes> RecommendationAttributes { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureDomainEntities();
        }
    }
}
