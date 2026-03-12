namespace Domain.Entities;

public class Feedback
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public int Mark { get; set; }
}
