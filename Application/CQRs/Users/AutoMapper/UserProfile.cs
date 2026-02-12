using Application.CQRs.Users.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRs.Users.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
