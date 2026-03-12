namespace Application.Abstract
{
    public interface IUserIdentityService
    {
        Task<IEnumerable<string>> GetUserRolesAsync(int userId, CancellationToken cancellationToken = default);
        Task<string?> GetUserNameAsync(int userId, CancellationToken cancellationToken = default);
    }
}
