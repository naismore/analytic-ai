using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IReadOnlyCollection<User>> GetListAsync();
        Task<User> GetByIdAsync(int id);
        Task InsertAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<int> SaveChangesAsync();
    }
}
