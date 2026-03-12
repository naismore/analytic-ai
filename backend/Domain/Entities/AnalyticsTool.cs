using Domain.Dtos;

namespace Domain.Entities
{
    public class AnalyticsTool
    {
        public int ToolId { get; set; }

        public string Name { get; set; }

        public ToolCategory Category { get; set; }
        public SkillLevel ComplexityLevel { get; set; }

        public int MaxDataVolume { get; set; }

        public List<RecommendationResult> RecommendationResults { get; set; } = new();

        public static AnalyticsTool Create(AnalyticToolDto dto)
        {
            return new AnalyticsTool
            {
                Name = dto.Name,
                Category = dto.ToolCategory,
                ComplexityLevel = dto.SkillLevel,
                MaxDataVolume = dto.MaxDataVolume,
            };
        }

        public static AnalyticsTool Edit(AnalyticsTool analyticsTool, AnalyticToolDto dto)
        {
            analyticsTool.Name = dto.Name;
            analyticsTool.Category = dto.ToolCategory;
            analyticsTool.ComplexityLevel = dto.SkillLevel;
            analyticsTool.MaxDataVolume = dto.MaxDataVolume;
            return analyticsTool;
        }
    }

}
