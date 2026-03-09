using Application.CQRs.Admin.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Admin;

namespace WebApi.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("create-analytic-tool")]
    public async Task<ActionResult> AddAnalyticTool([FromBody] AnalyticToolDto analyticToolDto)
    {
        var command = new CreateAnalyticToolCommand(
            analyticToolDto.Name, 
            analyticToolDto.ToolCategory, 
            analyticToolDto.SkillLevel,
            analyticToolDto.MaxDataVolume);

        await mediator.Send(command);
        return Ok();
    }

    [HttpPost]
    [Route("edit-analytic-tool/{id}")]
    public async Task<ActionResult> EditAnalyticTool([FromBody] AnalyticToolDto analyticToolDto, int id)
    {
        var command = new EditAnalyticToolCommand(
            id,
            analyticToolDto.Name,
            analyticToolDto.ToolCategory,
            analyticToolDto.SkillLevel,
            analyticToolDto.MaxDataVolume
            );
        await mediator.Send(command);
        return Ok();
    }
}
