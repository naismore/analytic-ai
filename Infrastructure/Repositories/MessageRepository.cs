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
    public class MessageRepository(DTADbContext context) : IMessageRepository
    {
        public async Task<IReadOnlyCollection<Message>> GetListAsync() =>
            await context.Messages.ToListAsync();
        public async Task<Message> GetByIdAsync(int id) =>
            await context.Messages.FindAsync(id);
        public async Task InsertAsync(Message message) =>
            await context.Messages.AddAsync(message);
        public void Update(Message message) =>
            context.Messages.Update(message);
        public void Delete(Message message) =>
            context.Messages.Remove(message);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
