namespace Domain.Entities
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public int DataVolume { get; set; }
        public List<int> DomainIds { get; set; } = new();
        public List<int> KnownToolIds { get; set; } = new();
        public DateTime LastUpdated { get; set; }
    }
}
