using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository(DTADbContext context) : IUserProfileRepository
    {
        public async Task<IReadOnlyCollection<UserProfile>> GetListAsync() =>
            await context.UserProfiles.ToListAsync();
        public async Task<UserProfile> GetByIdAsync(int id) =>
            await context.UserProfiles.FindAsync(id);
        public async Task InsertAsync(UserProfile userProfile) =>
            await context.UserProfiles.AddAsync(userProfile);
        public void Update(UserProfile userProfile) =>
            context.UserProfiles.Update(userProfile);
        public void Delete(UserProfile userProfile) =>
            context.UserProfiles.Remove(userProfile);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
