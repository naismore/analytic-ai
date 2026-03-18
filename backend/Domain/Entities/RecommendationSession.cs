namespace Domain.Entities;

public class RecommendationSession
{
    public Guid SessionId { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private RecommendationSession(int userId)
    {
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        SessionId = Guid.NewGuid();
    }

    public static RecommendationSession Create(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("UserId должен быть положительным", nameof(userId));

        return new RecommendationSession(userId);
    }
}
