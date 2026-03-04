using Application.Abstract;
using Application.CQRs.Results;

namespace Application.CQRs.Users.Commands
{
    public sealed record DeleteUserCommand(
        int UserId
        ) : ICommand<Result>;
}
