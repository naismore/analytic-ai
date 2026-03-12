namespace Application.CQRs.RecommendationSession.Dto;

public sealed record RecommendationSessionListDto(Guid SessionId, string Name, DateTime CreatedAt);