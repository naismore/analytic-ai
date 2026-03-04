using Application.Results;
using Application.Abstract;

namespace Application.CQRs.Users.Commands
{
    public sealed record DeleteUserCommand(
        int UserId
        ) : ICommand<Result>;
}
