using Application.Abstract;
using Application.CQRs.Auth.Commands;
using Application.CQRs.Auth.Dto;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.CQRs.Auth.Handlers;

public class RefreshTokenCommandHandler(
    ITokenService tokenService,
    IRefreshTokenRepository refreshTokenRepository,
    IUserIdentityService userIdentityService,
    ILogger<RefreshTokenCommandHandler> logger) : ICommandHandler<RefreshTokenCommand, LoginDto>
{
    public async Task<LoginDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var storedToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
        var tokens = await tokenService.RefreshTokensAsync(request.RefreshToken);

        var username = await userIdentityService.GetUserNameAsync(storedToken.UserId);

        return new LoginDto(
            storedToken.UserId,
            username,
            tokens.AccessToken,
            tokens.RefreshToken,
            tokens.AccessTokenExpiresAt,
            tokens.RefreshTokenExpiresAt
            );
    }
}