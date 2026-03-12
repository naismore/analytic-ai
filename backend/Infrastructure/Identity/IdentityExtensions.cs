using Infrastructure.Identity.Data.Config;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public static class IdentityExtensions
{
    public static void ConfigureIdentityOptions(IdentityOptions options, IdentitySettings settings)
    {
        // Password settings
        options.Password.RequireDigit = settings.Password.RequireDigit;
        options.Password.RequiredLength = settings.Password.RequiredLength;
        options.Password.RequireNonAlphanumeric = settings.Password.RequireNonAlphanumeric;
        options.Password.RequireUppercase = settings.Password.RequireUppercase;
        options.Password.RequireLowercase = settings.Password.RequireLowercase;

        // Lockout settings
        options.Lockout.MaxFailedAccessAttempts = settings.Lockout.MaxFailedAccessAttempts;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(settings.Lockout.DefaultLockoutMinutes);
        options.Lockout.AllowedForNewUsers = settings.Lockout.AllowedForNewUsers;

        // User settings
        options.User.RequireUniqueEmail = settings.User.RequireUniqueEmail;

        // SignIn settings
        options.SignIn.RequireConfirmedAccount = settings.SignIn.RequireConfirmedAccount;
        options.SignIn.RequireConfirmedEmail = settings.SignIn.RequireConfirmedEmail;
        options.SignIn.RequireConfirmedPhoneNumber = settings.SignIn.RequireConfirmedPhoneNumber;

        // Token providers
        options.Tokens.EmailConfirmationTokenProvider = settings.Token.EmailConfirmationTokenProvider;
        options.Tokens.PasswordResetTokenProvider = settings.Token.PasswordResetTokenProvider;
    }

    public static void ConfigureIdentityEntities(this ModelBuilder builder)
    {
        // Только Identity конфигурации!
        builder.ApplyConfiguration(new ApplicationUserConfiguration());
        builder.ApplyConfiguration(new ApplicationRoleConfiguration());

        // Если хочешь переименовать остальные Identity таблицы
        builder.Entity<IdentityUserRole<int>>().ToTable("user_roles");
        builder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<int>>().ToTable("user_logins");
        builder.Entity<IdentityUserToken<int>>().ToTable("user_tokens");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("role_claims");

        // Можно добавить индексы для часто запрашиваемых полей
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();
        });
    }
}
