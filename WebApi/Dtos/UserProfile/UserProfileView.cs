using Domain.Entities;

namespace WebApi.Dtos.UserProfile;

public record UserProfileView(SkillLevel SkillLevel, int DataVolume, DateTime LastUpdated);
