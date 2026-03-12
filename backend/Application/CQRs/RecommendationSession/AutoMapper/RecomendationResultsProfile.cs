using Application.CQRs.RecommendationSession.Dto;
using AutoMapper;
using Domain.Entities;

namespace Application.CQRs.RecommendationSession.AutoMapper
{
    public class RecomendationResultsProfile : Profile
    {
        public RecomendationResultsProfile()
        {
            CreateMap<RecommendationResult, SessionRecommendationResultsDto>();
            
        }
    }
}
