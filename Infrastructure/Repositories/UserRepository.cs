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
    internal class UserRepository(DTADbContext context) : IUserRepository
    {
        public async Task<IReadOnlyCollection<User>> GetListAsync() =>
            await context.Users.ToListAsync();
        public async Task<User> GetByIdAsync(int id) =>
            await context.Users.FindAsync(id);
        public async Task InsertAsync(User user) =>
            await context.Users.AddAsync(user);
        public void Update(User user) =>
            context.Users.Update(user);

        public void Delete(User user) =>
            context.Users.Remove(user);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
