namespace Infrastructure.Identity.Settings;

public class LockoutSettings
{
    public int MaxFailedAccessAttempts { get; set; } = 3;
    public int DefaultLockoutMinutes { get; set; } = 15;
    public bool AllowedForNewUsers { get; set; } = true;
}
