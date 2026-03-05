using Domain.Entities;

namespace Application.CQRs.UserProfile.Dtos;

public record UserProfileDto(SkillLevel SkillLevel, int DataVolume, DateTime LastUpdated);