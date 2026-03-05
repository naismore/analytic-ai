using Application.CQRs.UserProfile.Dtos;
using AutoMapper;

namespace Application.CQRs.UserProfile.AutoMapper;

public class UserProfileAutoMapperProfile : Profile
{
    public UserProfileAutoMapperProfile()
    {
        CreateMap<Domain.Entities.UserProfile, UserProfileDto>();
    }
}
