using Application.Abstract;
using Application.CQRs.RecommendationSession.Dto;
using Application.CQRs.RecommendationSession.Queries;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRs.RecommendationSession.Handlers;

public class GetRecommendationSessionsByUserIdQueryHandler(
    IRecommendationSessionRepository recommendationSessionRepository,
    IRecommendationAttributesRepository recommendationAttributesRepository,
    IEnumResolver enumResolver
) : IQueryHandler<GetRecommendationSessionsByUserIdQuery, IReadOnlyCollection<RecommendationSessionListDto>>
{
    public async Task<IReadOnlyCollection<RecommendationSessionListDto>> Handle(GetRecommendationSessionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        // 1. Получаем все сессии пользователя
        var recommendationSessions = await recommendationSessionRepository.GetByUserIdAsync(request.UserId);

        if (!recommendationSessions.Any())
            return Array.Empty<RecommendationSessionListDto>();

        // 2. Получаем sessionIds
        var sessionIds = recommendationSessions.Select(s => s.SessionId).ToList();

        // 3. Получаем атрибуты для этих сессий
        var attributeIds = recommendationAttributesRepository.GetBySessionIds(sessionIds);
        var attributes = await recommendationAttributesRepository.GetByIdsAsync(attributeIds);

        // 4. Формируем DTO
        var recommendationSessionDtos = recommendationSessions.Select(session =>
        {
            var attr = attributes.FirstOrDefault(a => a.SessionId == session.SessionId);

            // Если атрибуты есть, объединяем UserTasks через ", ", иначе оставляем пустую строку
            var userTasksStr = attr != null && attr.UserTasks.Any()
                ? string.Join(", ", attr.UserTasks.Select(x => enumResolver.Resolve(x)).ToList())
                : "New chat";

            return new RecommendationSessionListDto(
                session.SessionId,
                userTasksStr,
                session.CreatedAt
            );
        }).ToList();

        return recommendationSessionDtos;
    }
}