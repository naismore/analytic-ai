using Domain.Entities;

namespace Application.CQRs.Users.Dtos
{
    public sealed record UserDto(
        int UserId,
        UserProfile UserProfile,
        DateTime CreatedAt
        );
}
