using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;

namespace Application.CQRs.RecommendationSession.Queries
{
    public sealed record GetRecommendationSessionsByUserIdQuery(int UserId) : IQuery<IReadOnlyCollection<RecommendationSessionListDto>>;
}
