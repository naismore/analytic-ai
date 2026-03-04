using Application.Abstract;
using MediatR;

namespace Application.CQRs.Identity.Commands;

public record RegisterUserCommand(string username, string password) : ICommand<Unit>;
