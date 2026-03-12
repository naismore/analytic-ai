namespace WebApi.Dtos.Auth;

public sealed record UserLoginView(int UserId,
        string UserName,
        string AccessToken,
        DateTime AccessTokenExpiresAt,
        DateTime RefreshTokenExpiresAt);
