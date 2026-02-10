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
    internal class ConversationRepository(DTADbContext context) : IConversationRepository
    {
        public async Task<IReadOnlyCollection<Conversation>> GetListAsync() =>
            await context.Conversations.ToListAsync();
        public async Task<Conversation> GetByIdAsync(int id) =>
            await context.Conversations.FindAsync(id);
        public async Task InsertAsync(Conversation conversation) =>
            await context.Conversations.AddAsync(conversation);
        public void Update(Conversation conversation) =>
            context.Conversations.Update(conversation);
        public void Delete(Conversation conversation) =>
            context.Conversations.Remove(conversation);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
