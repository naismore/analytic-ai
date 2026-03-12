using Application.CQRs.RecommendationSession.Dto;
using Domain.Entities;

namespace Application.CQRs.RecommendationSession.Dto
{
    public record SessionRecommendationResultsDto(string ToolName, string ReasoningSummary, ResultType ResultType);
}