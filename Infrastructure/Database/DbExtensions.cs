using Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public static class DbExtensions
    {
        public static void ConfigureDomainEntities(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnalyticsToolConfiguration());
            builder.ApplyConfiguration(new RecommendationResultConfiguration());
            builder.ApplyConfiguration(new RecommendationSessionConfiguration());
            builder.ApplyConfiguration(new RecommendationAttributesConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
