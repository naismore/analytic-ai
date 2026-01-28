namespace Domain.Entities
{
    public class RecommendationResult
    {
        public int ToolId { get; set; }
        public int Rank { get; set; }
        public double Confidence { get; set; }
        public string ReasoningSummary { get; set; }
        public List<string> TradeOffs { get; set; }
    }
}
