namespace Application.Abstract
{
    public interface IAccessTokenService
    {
        Task<(string token, DateTime expiredAt)> GenerateTokenAsync(int userId, string userName, IEnumerable<string> roles);
        Task<bool> ValidateAccessTokenAsync(string accessToken);
    }
}
