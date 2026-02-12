using Application.Abstract;
using Application.CQRs.Users.Dtos;
using MediatR;

namespace Application.CQRs.Users.Queries
{
    public sealed record GetUserQuery(
        int UserId
        ) : IQuery<Result<UserDto>>;
}
