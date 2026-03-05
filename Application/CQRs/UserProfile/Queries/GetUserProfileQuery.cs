using Application.Abstract;
using Application.CQRs.UserProfile.Dtos;

namespace Application.CQRs.UserProfile.Queries;

public record GetUserProfileQuery(int UserId) : IQuery<UserProfileDto>;
