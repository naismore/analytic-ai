using Application.Abstract;
using Application.CQRs.UserProfile.Commands;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.CQRs.UserProfile.Handlers;

public class EditUserProfileCommandHandler(IUserProfileRepository userProfileRepository) : ICommandHandler<EditUserProfileCommand, Unit>
{
    public async Task<Unit> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await userProfileRepository.GetByUserIdAsync(request.UserId);

        if (userProfile == null)
            throw new Exception("UserProfile not found");

        var dto = new UserProfileDto(request.UserId, (SkillLevel)request.SkillLevel, request.DataVolume);

        userProfile = Domain.Entities.UserProfile.Update(userProfile, dto);

        userProfileRepository.Update(userProfile);
        
        await userProfileRepository.SaveChangesAsync();
        
        return Unit.Value;
    }
}