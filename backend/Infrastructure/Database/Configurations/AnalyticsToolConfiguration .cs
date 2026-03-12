using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
    public class AnalyticsToolConfiguration : IEntityTypeConfiguration<AnalyticsTool>
    {
        public void Configure(EntityTypeBuilder<AnalyticsTool> builder)
        {
            builder.ToTable("analytics_tools");
            builder.HasKey(t => t.ToolId);
            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(t => t.Category)
                   .IsRequired();

            builder.Property(t => t.ComplexityLevel)
                   .IsRequired();
        }
    }

}
