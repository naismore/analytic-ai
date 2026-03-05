using Application.Models;

namespace Application.Abstract
{
    public interface IAuthService
    {
        Task<(bool success, string[] errors)> RegisterAsync(
        string password,
        string userName,
        int userId,
        CancellationToken cancellationToken = default);

        Task<(bool success, int userId, TokenResponse tokens, string[] errors)> LoginAsync(
            string email,
            string password,
            CancellationToken cancellationToken = default);

        Task<bool> ChangePasswordAsync(
            int userId,
            string currentPassword,
            string newPassword,
            CancellationToken cancellationToken = default);
    }
}
