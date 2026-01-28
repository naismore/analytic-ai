namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public UserStatus UserStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
