namespace Domain.Entities
{
    public class RecommendationSession
    {
        public Guid SessionId { get; set; }

        public int UserId { get; set; }
        public string TaskDescription { get; set; } = string.Empty;

        public List<AnalyticsTool> AllowedTools { get; set; } = new();
        public List<RecommendationResult> Results { get; set; } = new();

        public SessionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
