namespace Infrastructure.Identity.Settings;

public class PasswordSettings
{
    public bool RequireDigit { get; set; } = true;
    public int RequiredLength { get; set; } = 8;
    public bool RequireNonAlphanumeric { get; set; } = true;
    public bool RequireUppercase { get; set; } = true;
    public bool RequireLowercase { get; set; } = true;
}
