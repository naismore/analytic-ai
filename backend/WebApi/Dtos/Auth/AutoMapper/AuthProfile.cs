using Application.CQRs.Auth.Dto;
using AutoMapper;

namespace WebApi.Dtos.Auth.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginDto, UserLoginView>();
        }
    }
}
