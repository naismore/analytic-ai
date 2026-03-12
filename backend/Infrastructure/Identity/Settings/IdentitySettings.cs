namespace Infrastructure.Identity.Settings;

public class IdentitySettings
{
    public const string SectionName = "Identity";

    public PasswordSettings Password { get; set; } = new();
    public LockoutSettings Lockout { get; set; } = new();
    public UserSettings User { get; set; } = new();
    public SignInSettings SignIn { get; set; } = new();
    public TokenSettings Token { get; set; } = new();
}
