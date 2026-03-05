using Application.Abstract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication.Services
{
    internal class AccessTokenService(
        IOptions<JwtSettings> jwtSettings,
        TokenValidationParameters tokenValidationParameters) : IAccessTokenService
    {
        public async Task<(string token, DateTime expiredAt)> GenerateTokenAsync(int userId, string userName, IEnumerable<string> roles)
        {
            var (accessToken, accessTokenExpiresAt) = GenerateAccessToken(userId, userName, roles);

            return (accessToken, accessTokenExpiresAt);
        }

        public async Task<bool> ValidateAccessTokenAsync(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                accessToken,
                tokenValidationParameters,
                out var validatedToken);

            // Проверяем, что это JWT токен с правильным алгоритмом
            if (validatedToken is JwtSecurityToken jwtToken)
            {
                var result = jwtToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);

                return result;
            }

            return true;
        }

        private (string token, DateTime expiresAt) GenerateAccessToken(
        int userId,
        string userName,
        IEnumerable<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new Claim("user_id", userId.ToString())
        };

            // Добавляем роли как claims
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(jwtSettings.Value.AccessTokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Value.Issuer,
                audience: jwtSettings.Value.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }

        
    }
}
