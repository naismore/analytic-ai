using Domain.Entities;

namespace Domain.Dtos;

public sealed record RecommendationAttributesDto(
    Guid SessionId,
    SkillLevel SkillLevel,
    DataVolume DataVolume,
    IEnumerable<UserTasks> UserTasks,
    Budget Budget,
    TechnicalBackground TechnicalBackground,
    IEnumerable<Integrations> Integrations
    );
