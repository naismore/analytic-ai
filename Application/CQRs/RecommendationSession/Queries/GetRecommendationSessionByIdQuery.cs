using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;

namespace Application.CQRs.RecommendationSession.Queries;

public sealed record GetRecommendationSessionByIdQuery(Guid SessionId) : IQuery<RecommendationSessionDto>;
