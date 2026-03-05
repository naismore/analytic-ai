using Application.Abstract;
using MediatR;

namespace Application.CQRs.UserProfile.Commands
{
    public record CreateUserProfileCommand(int UserId, int SkillLevel, int DataVolume) : ICommand<Unit>;
}
