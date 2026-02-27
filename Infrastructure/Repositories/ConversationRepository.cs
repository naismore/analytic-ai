using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;

namespace Infrastructure.Repositories
{
    public class ConversationRepository(DTADbContext context) : Repository<Conversation>(context), IConversationRepository
    {
    }
}
