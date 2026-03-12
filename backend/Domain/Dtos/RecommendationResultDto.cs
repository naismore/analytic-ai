using Domain.Entities;

namespace Domain.Dtos
{
    public record RecommendationResultDto(
        Guid SessionId,
        ResultType ResultType,
        int ToolId,
        double Confidence,
        string ReasoningSummary
        );
}
