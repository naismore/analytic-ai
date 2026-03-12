namespace Application.CQRs.RecommendationSession.Dto;

public sealed record RecommendationSessionDto(
    Guid SessionId,
    int SkillLevel,
    int DataVolume,
    int[] UserTasks,
    int Budget,
    int TechnicalBackground,
    int[] Integrations
    );
