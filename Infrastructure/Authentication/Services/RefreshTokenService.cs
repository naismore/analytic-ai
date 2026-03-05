using Application.Abstract;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Infrastructure.Authentication.Services;

public class RefreshTokenService(
    IOptions<JwtSettings> jwtSettings,
    IRefreshTokenRepository refreshTokenRepository,
    ILogger<RefreshTokenService> logger) : IRefreshTokenService
{
    public async Task<(string token, DateTime expiredAt)> GenerateTokenAsync(int userId)
    {
        var (refreshToken, refreshTokenExpiresAt) = GenerateRefreshToken();
        await refreshTokenRepository.CreateAsync(
                userId,
                refreshToken,
                refreshTokenExpiresAt);

        logger.LogInformation("Generated tokens for user {UserId}", userId);

        return (refreshToken, refreshTokenExpiresAt);
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken, string reason = "User initiated revoke")
    {
        await refreshTokenRepository.RevokeAsync(refreshToken, reason);
    }

    // RefreshToken service
    private (string token, DateTime expiresAt) GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        var token = Convert.ToBase64String(randomNumber);
        var expiresAt = DateTime.UtcNow.AddDays(jwtSettings.Value.RefreshTokenExpirationDays);

        return (token, expiresAt);
    }

    public async Task<RefreshToken> ValidateAndGetAsync(string token)
    {
        var storedToken = await refreshTokenRepository.GetByTokenAsync(token);

        if (storedToken == null)
        {
            logger.LogWarning("Refresh token not found: {Token}", token);
            throw new UnauthorizedAccessException("Invalid refresh token");
        }

        if (storedToken.RevokedAt != null)
        {
            logger.LogWarning("Refresh token revoked: {Token}", token);
            throw new UnauthorizedAccessException("Refresh token revoked");
        }

        if (storedToken.ExpiresAt <= DateTime.UtcNow)
        {
            logger.LogWarning("Refresh token expired: {Token}", token);
            throw new UnauthorizedAccessException("Refresh token expired");
        }

        return storedToken;
    }
}
