using Application.Abstract;
using Application.CQRs.Users.Commands;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.CQRs.Users.Handlers
{
    public class CreateUserCommandHandler(IUserRepository repository) : ICommandHandler<CreateUserCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User userForAdd = User.Create(request.UserStatus, request.CreatedAt);
            await repository.InsertAsync(userForAdd);
            int affectedRows = await repository.SaveChangesAsync();

            if (affectedRows > 0)
            {
                return Result<int>.Success(userForAdd.Id);
            }
            return Result<int>.Failure("Не удалось добавить пользователя");
        }
    }
}
