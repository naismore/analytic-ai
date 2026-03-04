using Application.Abstract;
using Application.CQRs.Results;
using Application.CQRs.Users.Commands;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.CQRs.Users.Handlers
{
    public class DeleteUserCommandHandler(IUserRepository repository) : ICommandHandler<DeleteUserCommand, Result>
    {
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? userFromDb = await repository.GetByIdAsync(request.UserId);
            if (userFromDb == null)
            {
                return Result.Failure("Пользователь не найден");
            }

            repository.Delete(userFromDb);
            int affectedRows = await repository.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return Result.Success();
            }

            return Result<int>.Failure("Не удалось удалить пользователя из базы данных");
        }
    }
}
