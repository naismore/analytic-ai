namespace Domain.Entities
{
    public class RecommendationResult
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string ToolName { get; set; }    
        public ResultType ResultType { get; set; }
        public string ReasoningSummary { get; set; }

        public virtual RecommendationSession Session { get; set; }
    }
}
