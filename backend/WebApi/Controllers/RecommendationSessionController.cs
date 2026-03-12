using Application.CQRs.RecommendationSession.Commands;
using Application.CQRs.RecommendationSession.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.RecommendationSession;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RecommendationSessionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize]
        [Route("create-new-rec-session")]
        public async Task<ActionResult> CreateNewRecommendationSession([FromBody] RecommendationSessionDto dto)
        {
            var command = new CreateNewRecommendationSessionCommand(
                dto.UserId,
                dto.SkillLevel,
                dto.DataVolume,
                dto.UserTasks,
                dto.Budget,
                dto.TeamSize,
                dto.TechnicalBackground,
                dto.Integrations
                );

            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("get-recommendation-sessions/{userId}")]
        public async Task<ActionResult> GetRecommendationSessions(int userId)
        {
            var query = new GetRecommendationSessionsByUserIdQuery(userId);
            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        [HttpGet]
        [Authorize]
        [Route("get-recommendation-session/{sessionId}")]
        public async Task<ActionResult> GetRecommendationSession(Guid sessionId)
        {
            var query = new GetRecommendationSessionByIdQuery(sessionId);
            var dto = await mediator.Send(query);
            return Ok(dto);
        }

    }
}
