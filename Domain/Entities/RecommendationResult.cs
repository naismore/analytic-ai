namespace Domain.Entities
{
    public class RecommendationResult
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public ResultType ResultType { get; set; }
        public int ToolId { get; set; }
        public double Confidence { get; set; }
        public string ReasoningSummary { get; set; }

        public virtual RecommendationSession Session { get; set; }
        public virtual AnalyticsTool Tool { get; set; }
    }
}
