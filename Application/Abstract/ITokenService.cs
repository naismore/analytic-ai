using Application.Models;

namespace Application.Abstract
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateTokensAsync(int userId, string userName, IEnumerable<string> roles);
        Task<TokenResponse> RefreshTokensAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
        Task<bool> ValidateAccessTokenAsync(string accessToken);
    }
}
