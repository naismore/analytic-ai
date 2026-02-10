using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<IReadOnlyCollection<UserProfile>> GetListAsync();
        Task<UserProfile> GetByIdAsync(int id);
        Task InsertAsync(UserProfile userProfile);
        void Update(UserProfile userProfile);
        void Delete(UserProfile userProfile);
        Task<int> SaveChangesAsync();
    }
}
