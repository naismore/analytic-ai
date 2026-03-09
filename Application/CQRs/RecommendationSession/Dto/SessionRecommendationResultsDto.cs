using Domain.Entities;

namespace Application.CQRs.RecommendationSession.Dto
{
    public record SessionRecommendationResultsDto(string ToolName, double Confidence, string ReasoningSummary, ResultType ResultType);
}
