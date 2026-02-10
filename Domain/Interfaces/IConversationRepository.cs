using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IConversationRepository
    {
        Task<IReadOnlyCollection<Conversation>> GetListAsync();
        Task<Conversation> GetByIdAsync(int id);
        Task InsertAsync(Conversation conversation);
        void Update(Conversation conversation);
        void Delete(Conversation conversation);
        Task<int> SaveChangesAsync();
    }
}
