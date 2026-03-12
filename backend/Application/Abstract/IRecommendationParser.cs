using Domain.Entities;

namespace Application.Abstract
{
    public interface IRecommendationParser
    {
        List<RecommendationResult> Parse(string text);
    }
}
