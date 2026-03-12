namespace Application.CQRs.Auth.Dto
{
    public record LoginDto(
        int UserId, 
        string UserName, 
        string AccessToken, 
        string RefreshToken, 
        DateTime AccessTokenExpiresAt, 
        DateTime RefreshTokenExpiresAt);
}
