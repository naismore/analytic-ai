using Domain.Entities;

namespace Domain.Dtos;

public sealed record RecommendationAttributesDto(
    Guid SessionId,
    SkillLevel SkillLevel,
    DataVolume DataVolume,
    List<UserTasks> UserTasks,
    Budget Budget,
    TeamSize TeamSize,
    TechnicalBackground TechnicalBackground,
    List<Integrations> Integrations
    );
