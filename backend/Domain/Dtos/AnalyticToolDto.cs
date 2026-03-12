using Domain.Entities;

namespace Domain.Dtos;

public record AnalyticToolDto(string Name, ToolCategory ToolCategory, SkillLevel SkillLevel, int MaxDataVolume);
