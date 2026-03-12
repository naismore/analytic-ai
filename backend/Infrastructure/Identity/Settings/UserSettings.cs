namespace Infrastructure.Identity.Settings;

public class UserSettings
{
    public bool RequireUniqueEmail { get; set; } = true;
    public int AllowedUserNameCharacters { get; set; } = 50;
}
