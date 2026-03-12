using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Repositories
{
    public class RefreshTokenRepository(DTADbContext context) : IRefreshTokenRepository
    {
       private IQueryable<RefreshToken> _baseQuery = context.RefreshTokens;
        public async Task<RefreshToken> CreateAsync(int userId, string token, DateTime expiresAt)
        {
            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = expiresAt
            };

            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();
            return refreshToken;
        }
        public async Task CleanupExpiredTokensAsync()
        {
            var expiredTokens = await _baseQuery.Where(rt => rt.ExpiresAt <=  DateTime.UtcNow).ToListAsync();

            context.RefreshTokens.RemoveRange(expiredTokens);
            await context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _baseQuery.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task RevokeAllUserTokensAsync(int userId, string reason = "Revoked")
        {
            var activeTokens = await context.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.RevokedAt == null && rt.ExpiresAt > DateTime.UtcNow)
                .ToListAsync();

            foreach (var activeToken in activeTokens)
            {
                activeToken.RevokedAt = DateTime.UtcNow;
                activeToken.RevokedReason = reason;
            }

            await context.SaveChangesAsync();
        }

        public async Task RevokeAsync(string token, string reason = "Revoked")
        {
            var refreshToken = await GetByTokenAsync(token);
            if (refreshToken != null)
            {
                refreshToken.ExpiresAt = DateTime.UtcNow;
                refreshToken.RevokedReason = reason;
                await context.SaveChangesAsync();
            }
        }
    }
}
