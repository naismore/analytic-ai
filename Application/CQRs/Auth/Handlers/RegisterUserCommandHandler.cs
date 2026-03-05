using Application.Abstract;
using Application.CQRs.Auth.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRs.Identity.Handlers;

public class RegisterUserCommandHandler(IUserRepository userRepository, IAuthService authService) : ICommandHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: Добавить валидацию
        User user = User.Create(UserStatus.Suspended, DateTime.UtcNow);

        await userRepository.InsertAsync(user);
        await userRepository.SaveChangesAsync();

        (bool success, string[] errors) = await authService.RegisterAsync(request.username, request.password, user.Id);
        if (errors.Any())
        {
            throw new Exception($"Не удалось создать нового пользователя. Ошибки: {string.Join(", ", errors)}");
        }

        return Unit.Value;
    }
}
