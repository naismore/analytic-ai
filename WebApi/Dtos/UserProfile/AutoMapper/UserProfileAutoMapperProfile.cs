using Application.CQRs.UserProfile.Dtos;
using AutoMapper;

namespace WebApi.Dtos.UserProfile.AutoMapper;

public class UserProfileAutoMapperProfile : Profile
{
    public UserProfileAutoMapperProfile()
    {
        CreateMap<Application.CQRs.UserProfile.Dtos.UserProfileDto, UserProfileView>();
    }
}
