
using Domain.Entities;

namespace Domain.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateAsync(int userId, string token, DateTime expiresAt);
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task RevokeAsync(string token, string reason = "Revoked");
    Task RevokeAllUserTokensAsync(int userId, string reason = "Revoked");
    Task CleanupExpiredTokensAsync();
}
