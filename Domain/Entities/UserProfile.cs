using Domain.DTO;

namespace Domain.Entities;

public class UserProfile
{
    public int UserId { get; set; }
    public SkillLevel SkillLevel { get; set; }
    public int DataVolume { get; set; }
    public DateTime LastUpdated { get; set; }

    public virtual User User { get; set; }

    public static UserProfile Create(UserProfileDto dto)
    {
        return new UserProfile
        {
            UserId = dto.UserId,
            SkillLevel = dto.SkillLevel,
            DataVolume = dto.DataVolume,
            LastUpdated = DateTime.UtcNow,
        };
    }

    public static UserProfile Update(UserProfile userProfile, UserProfileDto dto)
    {
        userProfile.SkillLevel = dto.SkillLevel;
        userProfile.DataVolume = dto.DataVolume;
        userProfile.LastUpdated = DateTime.UtcNow;
        return userProfile;
    }
}
