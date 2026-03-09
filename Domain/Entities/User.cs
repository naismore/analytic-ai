namespace Domain.Entities;

public class User
{
    public int Id { get; set; }

    public UserStatus UserStatus { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<RecommendationSession> RecommendationSessions { get; set; } = new();

    public static User Create(UserStatus userStatus, DateTime createdAt)
    {
        return new User()
        {
            UserStatus = userStatus,
            CreatedAt = createdAt
        };
    }
}