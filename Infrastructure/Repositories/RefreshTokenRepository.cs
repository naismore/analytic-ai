using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RefreshTokenRepository(DTADbContext context) : Repository<RefreshToken>(context), IRefreshTokenRepostiory
    {
        public async Task<RefreshToken> GetByTokenString(string tokenString) =>
            await Entities.FindAsync(tokenString);
    }
}
