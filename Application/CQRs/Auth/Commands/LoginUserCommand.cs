using Application.Abstract;
using Application.CQRs.Auth.Dto;

namespace Application.CQRs.Auth.Commands
{
    public record LoginUserCommand(string Username, string Password) : ICommand<LoginDto>;
}
