using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<IReadOnlyCollection<Message>> GetListAsync();
        Task<Message> GetByIdAsync(int id);
        Task InsertAsync(Message message);
        void Update(Message message);
        void Delete(Message message);
        Task<int> SaveChangesAsync();
    }
}
