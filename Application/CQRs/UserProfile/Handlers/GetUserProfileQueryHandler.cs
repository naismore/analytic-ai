using Application.Abstract;
using Application.CQRs.UserProfile.Dtos;
using Application.CQRs.UserProfile.Queries;
using AutoMapper;
using Domain.Interfaces;

namespace Application.CQRs.UserProfile.Handlers;

public class GetUserProfileQueryHandler(
    IUserProfileRepository userProfileRepository,
    IMapper mapper
    ) : IQueryHandler<GetUserProfileQuery, UserProfileDto?>
{
    public async Task<UserProfileDto?> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await userProfileRepository.GetByUserIdAsync(request.UserId);
        if (userProfile == null)
        {
            return null;
        }

        return mapper.Map<UserProfileDto>(userProfile);
    }
}
