using Application.Abstract;
using Application.CQRs.RecommendationSession.Commands;
using Application.CQRs.RecommendationSession.Dto;
using Application.Models;
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
        IMapper mapper,
        ILLMService llmService,
        IEnumResolver enumResolver,
        IRecommendationParser recommendationParser
        ) : ICommandHandler<CreateNewRecommendationSessionCommand, IReadOnlyCollection<SessionRecommendationResultsDto>>
    {
        public async Task<IReadOnlyCollection<SessionRecommendationResultsDto>> Handle(CreateNewRecommendationSessionCommand request, CancellationToken cancellationToken)
        {
            var userTasksforLLM = request.UserTasks.ToList().Select(ut => enumResolver.Resolve((UserTasks)ut)).ToArray();
            var integrationsforLLM = request.Integrations.ToList().Select(i => enumResolver.Resolve((Integrations)i)).ToArray();

            LLMRequest llmRequest = new LLMRequest(
                enumResolver.Resolve((SkillLevel)request.SkillLevel),
                enumResolver.Resolve((DataVolume)request.DataVolume),
                userTasksforLLM,
                enumResolver.Resolve((Budget)request.Budget),
                enumResolver.Resolve((TeamSize)request.TeamSize),
                enumResolver.Resolve((TechnicalBackground)request.TechnicalBackground),
                integrationsforLLM
                );
            
            var stringResponse = await llmService.SendRequestAsync(llmRequest);

            var recommendations = recommendationParser.Parse(stringResponse);

            // Сохраняем сессию
            var recommendationSessionDto = new Domain.Dtos.RecommendationSessionDto(
                request.UserId
                );

            var recommendationSession = Domain.Entities.RecommendationSession.Create(recommendationSessionDto);

            await recommendationSessionRepository.InsertAsync(recommendationSession);
            await recommendationSessionRepository.SaveChangesAsync();

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
                (TeamSize)request.TeamSize,
                (TechnicalBackground)request.TechnicalBackground,
                integrations
                );

            var recommendationAttributes = RecommendationAttributes.Create(recommendationAttributesDto);
            await recommendationAttributesRepository.InsertAsync(recommendationAttributes);
            await recommendationAttributesRepository.SaveChangesAsync();
           

            foreach(var rec in recommendations)
            {
                rec.SessionId = Guid.NewGuid();
                await recommendationResultRepository.InsertAsync(rec);
            }

            var recommendationsDto = recommendations.Select(x => mapper.Map<SessionRecommendationResultsDto>(x)).ToList();

            return recommendationsDto;
        }
    }
}
