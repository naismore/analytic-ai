using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;
using Application.CQRs.RecommendationSession.Queries;
using Domain.Interfaces;

namespace Application.CQRs.RecommendationSession.Handlers;

public class GetRecommendationSessionsByUserIdQueryHandler(
    IRecommendationSessionRepository recommendationSessionRepository,
    IRecommendationAttributesRepository recommendationAttributesRepository) : IQueryHandler<GetRecommendationSessionsByUserIdQuery, IReadOnlyCollection<RecommendationSessionListDto>>
{
    public async Task<IReadOnlyCollection<RecommendationSessionListDto>> Handle(GetRecommendationSessionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var recommendationSessions = await recommendationSessionRepository.GetByUserIdAsync(request.UserId);

        // TODO: Исправить хардкод
        var recommendationSessionDtos = recommendationSessions.ToList().Select(x => new RecommendationSessionListDto(x.SessionId, "Name", x.CreatedAt)).ToList();

        return recommendationSessionDtos;
    }
}
