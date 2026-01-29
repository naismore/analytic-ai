using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("user_profiles");
            builder.HasKey(p => p.UserId);

            builder.Property(p => p.SkillLevel)
                   .IsRequired();

            builder.Property(p => p.LastUpdated)
                   .IsRequired();
        }
    }

}
