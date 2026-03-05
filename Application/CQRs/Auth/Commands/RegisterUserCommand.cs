using Application.Abstract;
using MediatR;

namespace Application.CQRs.Auth.Commands;

public record RegisterUserCommand(string username, string password) : ICommand<Unit>;
