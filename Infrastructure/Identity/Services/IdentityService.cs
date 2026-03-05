using Application.Abstract;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Identity.Services;

public class UserIdentityService(UserManager<ApplicationUser> userManager, ILogger<UserIdentityService> logger) : IUserIdentityService
{
    public async Task<IEnumerable<string>> GetUserRolesAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            // Ищем Identity пользователя по BusinessUserId
            var identityUser = await userManager.Users
                .FirstOrDefaultAsync(u => u.BusinessUserId == userId, cancellationToken);

            if (identityUser == null)
            {
                logger.LogWarning("Identity user not found for BusinessUserId: {UserId}", userId);
                return Enumerable.Empty<string>();
            }

            // Получаем роли через UserManager
            var roles = await userManager.GetRolesAsync(identityUser);

            return roles;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting roles for user {UserId}", userId);
            return Enumerable.Empty<string>();
        }
    }

    public async Task<string?> GetUserNameAsync(int userId, CancellationToken cancellationToken = default)
    {
        var identityUser = await userManager.Users
            .FirstOrDefaultAsync(u => u.BusinessUserId == userId, cancellationToken);

        return identityUser?.UserName;
    }
}
