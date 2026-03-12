using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Database.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Infrastructure.Repositories
{
    public class RecommendationAttributesRepository(DTADbContext context) : Repository<RecommendationAttributes>(context), IRecommendationAttributesRepository
    {
        public async Task<IReadOnlyCollection<RecommendationAttributes>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var idsHashSet = ids.ToHashSet();

            return await Entities
                .Where(x => idsHashSet.Contains(x.Guid))
                .ToListAsync();
        }

        public IReadOnlyCollection<Guid> GetBySessionIds(IEnumerable<Guid> sessionIds)
        {
            var sessionIdsSet = sessionIds.ToHashSet(); // для быстрого поиска
            return Entities
                .Where(x => sessionIdsSet.Contains(x.SessionId)) // пересечение с переданными sessionIds
                .Select(x => x.Guid) // возвращаем ID аттрибута
                .ToList();
        }
    }
}
