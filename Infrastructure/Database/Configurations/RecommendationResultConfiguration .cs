using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
    public class RecommendationResultConfiguration : IEntityTypeConfiguration<RecommendationResult>
    {
        public void Configure(EntityTypeBuilder<RecommendationResult> builder)
        {
            builder.ToTable("recommendation_results");
            builder.HasKey(r => r.Id);


            builder.HasOne(r => r.Session)
                   .WithMany(s => s.Results)
                   .HasForeignKey(r => r.SessionId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }

}
