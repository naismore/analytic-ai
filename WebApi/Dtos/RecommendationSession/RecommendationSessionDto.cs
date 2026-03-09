namespace WebApi.Dtos.RecommendationSession
{
    public sealed record RecommendationSessionDto(
        int UserId,
        int SkillLevel, // Required
        int DataVolume, // Required
        int[] UserTasks, // Required
        int Budget, // Required
        int TeamSize, // Required
        int TechnicalBackground,
        int[] Integrations
        );
}