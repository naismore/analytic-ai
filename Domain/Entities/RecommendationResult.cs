namespace Domain.Entities
{
    public class RecommendationResult
    {
        public int Id { get; set; }

        public Guid SessionId { get; set; }
        public RecommendationSession Session { get; set; }

        public int ToolId { get; set; }
        public AnalyticsTool Tool { get; set; }

        public int Rank { get; set; }
        public double Confidence { get; set; }

        public string ReasoningSummary { get; set; }
    }

}
