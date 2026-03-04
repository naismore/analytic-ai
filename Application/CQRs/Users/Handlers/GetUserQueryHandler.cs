using Application.Abstract;
using Application.CQRs.Users.Dtos;
using Application.CQRs.Users.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.CQRs.Users.Handlers
{
    public class GetUserQueryHandler(IUserRepository repository, IMapper mapper) : IQueryHandler<GetUserQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            User? userFromDb = await repository.GetByIdAsync(request.UserId);
            if (userFromDb == null)
            {
                return Result<UserDto>.Failure("Пользователь не найден");
            }
            return Result<UserDto>.Success(mapper.Map<UserDto>(userFromDb));
        }
    }
}
