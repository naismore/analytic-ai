//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using System.Security.Cryptography;
//using Domain.Entities;

//namespace WebApi.Utility
//{
//    public class TokensProvider(IOptions<JwtTokenOptions> jwtOptions, IOptions<RefreshTokenOptions> refreshOptions)
//    {
//        private readonly JwtTokenOptions _jwtOptions = jwtOptions.Value;
//        private readonly RefreshTokenOptions _refreshOptions = refreshOptions.Value;
//        private const int _refreshTokenLength = 32;

//        public string GenerateAccessToken(ApplicationUser applicationUser)
//        {
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
//            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                signingCredentials: signingCredentials,
//                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes));

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        public string GenerateRefreshToken(ApplicationUser applicationUser)
//        {
//            byte[] token = new byte[_refreshTokenLength];
//            RandomNumberGenerator rng = RandomNumberGenerator.Create();
//            rng.GetBytes(token);
//            string tokenString = token.ToString();

//            RefreshToken refreshToken = new RefreshToken()
//            {
//                TokenString = tokenString,
//                UserId = applicationUser.Id,
//                User = applicationUser,
//                ExpiryDate = DateTime.UtcNow.AddDays(refreshOptions.Value.ExpiresDays),
//                IsRevoked = false
//            };

//            return tokenString;
//        }
//    }
//}
