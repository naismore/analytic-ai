namespace Domain.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public Roles Role { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
