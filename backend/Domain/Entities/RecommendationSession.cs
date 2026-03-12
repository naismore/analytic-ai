using Domain.Dtos;

namespace Domain.Entities
{
    public class RecommendationSession
    {
        public Guid SessionId { get; set; }
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }

        public virtual List<RecommendationResult> Results { get; set; }

        public static RecommendationSession Create(RecommendationSessionDto recommendationSessionDto)
        {
            return new RecommendationSession
            {
                UserId = recommendationSessionDto.UserId,
                CreatedAt = DateTime.UtcNow,
            };
        }
    }
}
