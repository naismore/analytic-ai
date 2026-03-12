using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;

namespace Application.CQRs.RecommendationSession.Commands;

public sealed record CreateNewRecommendationSessionCommand(
    int UserId,
    int SkillLevel,
    int DataVolume,
    int[] UserTasks,
    int Budget,
    int TeamSize,
    int TechnicalBackground,
    int[] Integrations
    ) : ICommand<IReadOnlyCollection<SessionRecommendationResultsDto>>;
