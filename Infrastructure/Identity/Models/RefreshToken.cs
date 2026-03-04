namespace Infrastructure.Identity.Models;

public class RefreshToken
{
    public string TokenString { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
}
