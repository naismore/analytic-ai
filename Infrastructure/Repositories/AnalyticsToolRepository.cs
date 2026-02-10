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
    public class AnalyticsToolRepository(DTADbContext context) : IAnalyticsToolRepository
    {
        public async Task<IReadOnlyCollection<AnalyticsTool>> GetListAsync() =>
            await context.AnalyticsTools.ToListAsync();
        public async Task<AnalyticsTool> GetByIdAsync(int id) =>
            await context.AnalyticsTools.FindAsync(id);
        public async Task InsertAsync(AnalyticsTool analyticsTool) =>
            await context.AnalyticsTools.AddAsync(analyticsTool);
        public void Update(AnalyticsTool analyticsTool) => 
            context.AnalyticsTools.Update(analyticsTool);

        public void Delete(AnalyticsTool analyticsTool) =>
            context.AnalyticsTools.Remove(analyticsTool);
        public async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();

    }
}