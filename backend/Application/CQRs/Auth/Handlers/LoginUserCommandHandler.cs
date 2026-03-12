using Application.Abstract;
using Application.CQRs.Auth.Commands;
using Application.CQRs.Auth.Dto;
using Microsoft.Extensions.Logging;

namespace Application.CQRs.Identity.Handlers
{
    public class LoginUserCommandHandler(
        IAuthService authService,
        ILogger<LoginUserCommandHandler> logger) : ICommandHandler<LoginUserCommand, LoginDto>
    {
        public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var (success, userId, tokens, errors) = await authService.LoginAsync(request.Username, request.Password);

            if (!success)
            {
                logger.LogWarning("Failed login attempt for {Username}", request.Username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            logger.LogInformation("User {UserId} logged in successfully", userId);

            return new LoginDto
            (
                userId,
                request.Username,
                tokens.AccessToken,
                tokens.RefreshToken,
                tokens.AccessTokenExpiresAt,
                tokens.RefreshTokenExpiresAt
            );
        }
    }
}
