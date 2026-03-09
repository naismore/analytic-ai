using Application.CQRs.RecommendationSession.Commands;
using Application.CQRs.RecommendationSession.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.RecommendationSession;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RecommendationSessionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("create-new-rec-session")]
        public async Task<ActionResult> CreateNewRecommendationSession([FromBody] RecommendationSessionDto dto)
        {
            var command = new CreateNewRecommendationSessionCommand(
                dto.UserId,
                dto.SkillLevel,
                dto.DataVolume,
                dto.UserTasks,
                dto.Budget,
                dto.TechnicalBackground,
                dto.Integrations
                );

            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("get-recommendation-sessions/{userId}")]
        public async Task<ActionResult> GetRecommendationSessions([FromQuery] int userId)
        {
            var query = new GetRecommendationSessionsByUserIdQuery(userId);
            var dto = await mediator.Send(query);
            return Ok(dto);
        }
    }
}
