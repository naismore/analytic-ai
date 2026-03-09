using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
    public class RecommendationSessionConfiguration : IEntityTypeConfiguration<RecommendationSession>
    {
        public void Configure(EntityTypeBuilder<RecommendationSession> builder)
        {
            builder.ToTable("recommendation_sessions");

            builder.HasKey(s => s.SessionId);
        }
    }

}
