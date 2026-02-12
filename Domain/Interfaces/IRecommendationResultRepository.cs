using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRecommendationResultRepository
    {
        Task<IReadOnlyCollection<RecommendationResult>> GetListAsync();
        Task<RecommendationResult?> GetByIdAsync(int id);
        Task InsertAsync(RecommendationResult recommendationResult);
        void Update(RecommendationResult recommendationResult);
        void Delete(RecommendationResult recommendationResultanalyticsTool);
        Task<int> SaveChangesAsync();
    }
}
