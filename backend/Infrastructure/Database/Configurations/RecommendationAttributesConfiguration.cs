using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
    public class RecommendationAttributesConfiguration : IEntityTypeConfiguration<RecommendationAttributes>
    {
        public void Configure(EntityTypeBuilder<RecommendationAttributes> builder)
        {
            builder.HasKey(x => x.Guid);
            builder.ToTable("recommendation_attributes");
        }
    }
}
