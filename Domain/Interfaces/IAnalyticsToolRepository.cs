using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAnalyticsToolRepository
    {
        public Task<IReadOnlyCollection<AnalyticsTool>> GetListAsync();
        public Task<AnalyticsTool?> GetByIdAsync(int id);
        public Task InsertAsync(AnalyticsTool analyticsTool);
        public void Update(AnalyticsTool analyticsTool);
        public void Delete(AnalyticsTool analyticsTool);
        public Task<int> SaveChangesAsync();
    }
}
