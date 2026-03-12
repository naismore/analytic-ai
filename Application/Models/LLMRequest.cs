namespace Application.Models;

public record LLMRequest(
    string SkillLevel,
    string DataVolume,
    string[] UserTasks,
    string Budget,
    string TechnicalBackground,
    string[] Integrations);
