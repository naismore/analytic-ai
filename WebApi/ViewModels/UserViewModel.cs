using Domain.Entities;

namespace WebApi.ViewModels
{
    public sealed record UserViewModel(
        int Id,
        UserStatus UserStatus,
        DateTime CreatedAt
        );
}
