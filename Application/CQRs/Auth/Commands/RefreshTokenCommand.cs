using Application.Abstract;
using Application.CQRs.Auth.Dto;

namespace Application.CQRs.Auth.Commands;

public record RefreshTokenCommand(string RefreshToken) : ICommand<LoginDto>;
