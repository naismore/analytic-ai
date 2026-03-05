using Application.Abstract;
using Application.Models;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Identity.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AuthService> logger,
        ITokenService tokenService
        ) : IAuthService
    {
        public Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool success, int userId, TokenResponse tokens, string[] errors)> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            var identityUser = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);

            if (identityUser == null)
            {
                logger.LogWarning("Login failed: user not found {Username}", username);
                return (false, 0, null!, new[] { "Invalid username or password" });
            }

            var passwordValid = await userManager.CheckPasswordAsync(identityUser, password);
            if (!passwordValid)
            {
                logger.LogWarning("Login failed: invalid password for {Username}", username);
                return (false, 0, null!, new[] { "Invalid username or password" });
            }

            var roles = await userManager.GetRolesAsync(identityUser);

            var tokens = await tokenService.GenerateTokensAsync(
                identityUser.BusinessUserId,
                identityUser.UserName,
                roles);

            logger.LogInformation("User {UserId} logged in successfully", identityUser.BusinessUserId);

            return (true, identityUser.BusinessUserId, tokens, Array.Empty<string>());
        }

        public async Task<(bool success, string[] errors)> RegisterAsync(
            string username, 
            string password, 
            int userId, 
            CancellationToken cancellationToken = default)
        {
            if (userId == 0)
            {
                return (false, new[] { "Business user not found" });
            }

            var identityUser = new ApplicationUser
            {
                UserName = username,
                BusinessUserId = userId
            };

            try
            {
                var result = await userManager.CreateAsync(identityUser, password);
                if (!result.Succeeded)
                {
                    return (false, result.Errors.Select(e => e.Description).ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return (true, Array.Empty<string>());

        }
    }
}
