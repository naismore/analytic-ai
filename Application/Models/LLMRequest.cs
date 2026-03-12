namespace Application.Models;

public record LLMRequest(
    string SkillLevel,
    string DataVolume,
    string[] UserTasks,
    string Budget,
    string TeamSize,
    string TechnicalBackground,
    string[] Integrations);
