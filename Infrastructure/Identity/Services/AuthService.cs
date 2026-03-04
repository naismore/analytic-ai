using Application.Abstract;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        ) : IAuthService
    {
        public Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, int userId, string token, string error)> LoginAsync(string username, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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

            return (true, []);

        }
    }
}
