using Domain.Entities;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Database
{
    public class DTADbContext(DbContextOptions<DTADbContext> options) : DbContext(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<AnalyticsTool> AnalyticsTools { get; set; } = null!;
        public DbSet<RecommendationSession> RecommendationSessions { get; set; } = null!;
        public DbSet<RecommendationResult> RecommendationResults { get; set; } = null!;
        public DbSet<Conversation> Conversations { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureDomainEntities();
        }
    }
}
