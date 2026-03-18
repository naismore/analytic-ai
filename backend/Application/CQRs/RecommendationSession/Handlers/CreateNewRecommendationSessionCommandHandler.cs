using Application.Abstract;
using Application.CQRs.RecommendationSession.Commands;
using Application.CQRs.RecommendationSession.Dto;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.CQRs.RecommendationSession.Handlers;

public class CreateNewRecommendationSessionCommandHandler(
    IRecommendationSessionRepository sessionRepository,
    IRecommendationAttributesRepository attributesRepository,
    IRecommendationResultRepository resultRepository,
    IMapper mapper,
    ILLMService llmService,
    IEnumResolver enumResolver,
    IRecommendationParser parser
) : ICommandHandler<CreateNewRecommendationSessionCommand, IReadOnlyCollection<SessionRecommendationResultsDto>>
{
    public async Task<IReadOnlyCollection<SessionRecommendationResultsDto>> Handle(
        CreateNewRecommendationSessionCommand request,
        CancellationToken cancellationToken)
    {
        // Подготовка запроса для LLM
        var llmRequest = new LLMRequest(
            enumResolver.Resolve((SkillLevel)request.SkillLevel),
            enumResolver.Resolve((DataVolume)request.DataVolume),
            request.UserTasks.Select(ut => enumResolver.Resolve((UserTasks)ut)).ToArray(),
            enumResolver.Resolve((Budget)request.Budget),
            enumResolver.Resolve((TeamSize)request.TeamSize),
            enumResolver.Resolve((TechnicalBackground)request.TechnicalBackground),
            request.Integrations.Select(i => enumResolver.Resolve((Integrations)i)).ToArray()
        );

        var llmResponse = await llmService.SendRequestAsync(llmRequest);

        var jsonDoc = JsonDocument.Parse(llmResponse);
        string text = jsonDoc.RootElement.GetProperty("result").GetString();

        // Парсим рекомендации
        var recommendations = parser.Parse(text);

        // Создаем сессию через Domain
        var session = Domain.Entities.RecommendationSession.Create(request.UserId);
        await sessionRepository.InsertAsync(session);
        await sessionRepository.SaveChangesAsync();

        // Создаем атрибуты через Domain
        var attributes = RecommendationAttributes.Create(
            session.SessionId,
            (SkillLevel)request.SkillLevel,
            (DataVolume)request.DataVolume,
            request.UserTasks.Select(ut => (UserTasks)ut),
            (Budget)request.Budget,
            (TeamSize)request.TeamSize,
            (TechnicalBackground)request.TechnicalBackground,
            request.Integrations.Select(i => (Integrations)i)
        );

        await attributesRepository.InsertAsync(attributes);
        await attributesRepository.SaveChangesAsync();

        // Сохраняем результаты
        foreach (var rec in recommendations)
        {
            rec.SessionId = session.SessionId; // связываем сессией
            await resultRepository.InsertAsync(rec);
        }
        await resultRepository.SaveChangesAsync();

        // Маппинг для ответа
        return recommendations
            .Select(mapper.Map<SessionRecommendationResultsDto>)
            .ToList();
    }
}