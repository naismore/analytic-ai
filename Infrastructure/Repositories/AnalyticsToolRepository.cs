using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;

namespace Infrastructure.Repositories
{
    public class AnalyticsToolRepository(DTADbContext context) : Repository<AnalyticsTool>(context), IAnalyticsToolRepository
    {
    }
}