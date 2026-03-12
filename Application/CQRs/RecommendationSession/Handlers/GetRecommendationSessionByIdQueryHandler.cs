using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;
using Application.CQRs.RecommendationSession.Queries;
using Domain.Interfaces;

namespace Application.CQRs.RecommendationSession.Handlers;

public class GetRecommendationSessionByIdQueryHandler(
    IRecommendationAttributesRepository recommendationAttributesRepository) : IQueryHandler<GetRecommendationSessionByIdQuery, RecommendationSessionDto>
{
    public async Task<RecommendationSessionDto> Handle(GetRecommendationSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var recommendationAttributeId = recommendationAttributesRepository.GetBySessionIds(new[] { request.SessionId }).First();
        var recommendationAttributes = await recommendationAttributesRepository.GetByIdsAsync(new[] { recommendationAttributeId });
        var attribute = recommendationAttributes.ToList().First();

        var userTasks = attribute.UserTasks.Select(ut => (int)ut).ToArray();
        var integrations = attribute.Integrations.Select(i => (int)i).ToArray();

        return new RecommendationSessionDto(
            request.SessionId,
            (int)attribute.SkillLevel,
            (int)attribute.DataVolume,
            userTasks,
            (int)attribute.Budget,
            (int)attribute.TeamSize,
            (int)attribute.TechnicalBackground,
            integrations
            );

    }
}
