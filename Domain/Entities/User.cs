namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public UserStatus UserStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserProfile Profile { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<Conversation> Conversations { get; set; } = new();
        public List<RecommendationSession> RecommendationSessions { get; set; } = new();
    }

}