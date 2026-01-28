namespace Domain.Entities
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public int DataVolume { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
