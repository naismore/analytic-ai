using Domain.Entities;

namespace Domain.DTO
{
    public record UserProfileDto(int UserId, SkillLevel SkillLevel, int DataVolume);
}
