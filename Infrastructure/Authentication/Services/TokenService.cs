using Application.Abstract;
using Application.Models;

namespace Infrastructure.Authentication.Services
{
    public class TokenService(
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService,
        IUserIdentityService userIdentityService) : ITokenService
    {
        public async Task<TokenResponse> GenerateTokensAsync(int userId, string userName, IEnumerable<string> roles)
        {
            var (accessToken, accessTokenExpireAt) = await accessTokenService.GenerateTokenAsync(userId, userName, roles);
            var (refreshToken, refreshTokenExpireAt) = await refreshTokenService.GenerateTokenAsync(userId);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiresAt = accessTokenExpireAt,
                RefreshTokenExpiresAt = refreshTokenExpireAt,
            };
        }

        public async Task<TokenResponse> RefreshTokensAsync(string refreshToken)
        {
            var storedToken = await refreshTokenService.ValidateAndGetAsync(refreshToken);
            var userId = storedToken.UserId;

            var username = await userIdentityService.GetUserNameAsync(userId);
            var roles = await userIdentityService.GetUserRolesAsync(userId);

            await refreshTokenService.RevokeRefreshTokenAsync(refreshToken, "RefreshToken replaced by new");

            return await GenerateTokensAsync(userId, username, roles);
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken) => 
            await refreshTokenService.RevokeRefreshTokenAsync(refreshToken);

        public async Task<bool> ValidateAccessTokenAsync(string accessToken) =>
            await accessTokenService.ValidateAccessTokenAsync(accessToken);
    }
}
