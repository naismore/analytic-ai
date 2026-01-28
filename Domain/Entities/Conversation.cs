namespace Domain.Entities
{
    public class Conversation
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ConversationType Type { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public List<Message> Messages { get; set; } = new();
    }


}
