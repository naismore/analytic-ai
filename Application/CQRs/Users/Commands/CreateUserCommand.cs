using Application.Abstract;
using Domain.Entities;

namespace Application.CQRs.Users.Commands
{
    public sealed record CreateUserCommand(
        UserStatus UserStatus,
        DateTime CreatedAt
        ) : ICommand<Result<int>>;
}
