namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }

        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        public Roles Role { get; set; }
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
