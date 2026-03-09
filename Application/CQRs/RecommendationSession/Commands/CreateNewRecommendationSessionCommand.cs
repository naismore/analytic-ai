using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;
using Domain.Entities;

namespace Application.CQRs.RecommendationSession.Commands
{
    public record CreateNewRecommendationSessionCommand(
        int UserId,
        int SkillLevel,
        int DataVolume,
        int[] UserTasks,
        int Budget,
        int TechnicalBackground,
        int[] Integrations
        ) : ICommand<IReadOnlyCollection<SessionRecommendationResultsDto>>;
}
