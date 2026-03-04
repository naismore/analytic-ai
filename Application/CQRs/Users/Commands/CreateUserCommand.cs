using Application.Abstract;
using Application.Users.Dtos;
using Domain.Entities;

namespace Application.CQRs.Users.Commands
{
    public sealed record CreateUserCommand(
        UserStatus UserStatus,
        DateTime CreatedAt
        ) : ICommand<Result<int>>;
}
