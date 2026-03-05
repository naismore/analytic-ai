using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Settings;

public class TokenSettings
{
    public string EmailConfirmationTokenProvider { get; set; } = TokenOptions.DefaultEmailProvider;
    public string PasswordResetTokenProvider { get; set; } = TokenOptions.DefaultEmailProvider;
    public int TokenLifespanHours { get; set; } = 24;
}
