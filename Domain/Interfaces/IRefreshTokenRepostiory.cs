using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRefreshTokenRepostiory
    {
        public Task<RefreshToken> GetByTokenString(string tokenString);
    }
}
