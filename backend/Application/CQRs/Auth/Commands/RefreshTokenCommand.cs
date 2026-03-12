using Application.Abstract;
using Application.CQRs.Auth.Dto;

namespace Application.CQRs.Auth.Commands;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<LoginDto>;
