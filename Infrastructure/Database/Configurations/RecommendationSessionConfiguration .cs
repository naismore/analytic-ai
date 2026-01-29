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

            builder.Property(s => s.TaskDescription)
                   .IsRequired()
                   .HasMaxLength(2000);

            builder.Property(s => s.Status)
                   .IsRequired();

            builder.HasOne(s => s.User)
                   .WithMany(u => u.RecommendationSessions)
                   .HasForeignKey(s => s.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Conversation)
                   .WithMany()
                   .HasForeignKey(s => s.ConversationId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
