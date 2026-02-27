using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;

namespace Infrastructure.Repositories
{
    public class MessageRepository(DTADbContext context) : Repository<Message>(context), IMessageRepository
    {
    }
}
