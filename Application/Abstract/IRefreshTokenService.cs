using Domain.Entities;

namespace Application.Abstract
{
    public interface IRefreshTokenService
    {
        Task<(string token, DateTime expiredAt)> GenerateTokenAsync(int userId);
        Task RevokeRefreshTokenAsync(string token, string reason = "User initiated revoke");
        Task<RefreshToken> ValidateAndGetAsync(string token);
    }
}
