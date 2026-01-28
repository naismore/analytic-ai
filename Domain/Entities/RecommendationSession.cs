namespace Domain.Entities
{
    public class RecommendationSession
    {
        public Guid SessionId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Guid? ConversationId { get; set; }
        public Conversation? Conversation { get; set; }

        public string TaskDescription { get; set; }
        public SessionStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<RecommendationResult> Results { get; set; } = new();
    }


}
