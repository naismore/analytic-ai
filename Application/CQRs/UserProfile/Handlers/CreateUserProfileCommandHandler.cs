using Application.Abstract;
using Application.CQRs.UserProfile.Commands;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRs.UserProfile.Handlers
{
    public class CreateUserProfileCommandHandler(
        IUserRepository userRepository,
        IUserProfileRepository userProfileRepository) : ICommandHandler<CreateUserProfileCommand, Unit>
    {
        public async Task<Unit> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user == null)
                throw new Exception("User not found!");

            var userProfile = await userProfileRepository.GetByUserIdAsync(request.UserId);

            if (userProfile != null)
                throw new Exception("User already has UserProfile");

            var createUserProfileDto = new UserProfileDto(
                request.UserId,
                (SkillLevel)request.SkillLevel,
                request.DataVolume);

            var newUserProfile = Domain.Entities.UserProfile.Create(createUserProfileDto);

            await userProfileRepository.InsertAsync(newUserProfile);
            await userProfileRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
