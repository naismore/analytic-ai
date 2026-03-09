using Application.Abstract;
using Application.CQRs.RecommendationSession.Commands;
using Application.CQRs.RecommendationSession.Dto;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.CQRs.RecommendationSession.Handlers
{
    public class CreateNewRecommendationSessionCommandHandler(
        IRecommendationSessionRepository recommendationSessionRepository,
        IRecommendationResultRepository recommendationResultRepository,
        IRecommendationAttributesRepository recommendationAttributesRepository,
        IMapper mapper
        ) : ICommandHandler<CreateNewRecommendationSessionCommand, IReadOnlyCollection<SessionRecommendationResultsDto>>
    {
        public async Task<IReadOnlyCollection<SessionRecommendationResultsDto>> Handle(CreateNewRecommendationSessionCommand request, CancellationToken cancellationToken)
        {
            // Здесь делаем запрос к API LLM через свой сервис

            // Сохраняем сессию
            var recommendationSessionDto = new RecommendationSessionDto(
                request.UserId
                );

            var recommendationSession = Domain.Entities.RecommendationSession.Create(recommendationSessionDto);

            await recommendationSessionRepository.InsertAsync(recommendationSession);
            await recommendationSessionRepository.SaveChangesAsync();

            var mainRecommendation = new SessionRecommendationResultsDto("Toolname1", 0.95, "Инструмент соответствует уровню Junior", ResultType.Main);
            var alternativeRecommendation1 = new SessionRecommendationResultsDto("Toolname2", 0.85, "Более продвинутые возможности визуализации", ResultType.Alternative);
            var alternativeRecommendation2 = new SessionRecommendationResultsDto("Toolname3", 0.75, "Корпоративное решение с расширенными функциями", ResultType.Alternative);

            if (recommendationSession.SessionId == Guid.Empty)
                throw new Exception("Не удалось добавить recommendationSession");

            List<UserTasks> userTasks = request.UserTasks.ToList().Select(ut => (UserTasks)ut).ToList();
            List<Integrations> integrations = request.Integrations.ToList().Select(i => (Integrations)i).ToList();


            // Сохраняем аттрибуты
            var recommendationAttributesDto = new RecommendationAttributesDto(
                recommendationSession.SessionId,
                (SkillLevel)request.SkillLevel,
                (DataVolume)request.DataVolume,
                userTasks,
                (Budget)request.Budget,
                (TechnicalBackground)request.TechnicalBackground,
                integrations
                );

            var recommendationAttributes = RecommendationAttributes.Create(recommendationAttributesDto);
            await recommendationAttributesRepository.InsertAsync(recommendationAttributes);
            await recommendationAttributesRepository.SaveChangesAsync();

            // Сохраняем результаты



            return [mainRecommendation, alternativeRecommendation1, alternativeRecommendation2];
        }
    }
}
