namespace Application.CQRs.RecommendationSession.Dto;

public sealed record RecommendationSessionResponseDto(Guid SessionId, SessionRecommendationResultsDto[] Results);
