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
    }

}
