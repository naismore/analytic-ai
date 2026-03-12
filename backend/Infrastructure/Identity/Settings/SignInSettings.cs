namespace Infrastructure.Identity.Settings;

public class SignInSettings
{
    public bool RequireConfirmedAccount { get; set; } = true;
    public bool RequireConfirmedEmail { get; set; } = true;
    public bool RequireConfirmedPhoneNumber { get; set; } = false;
}
