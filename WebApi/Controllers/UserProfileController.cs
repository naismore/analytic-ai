using Application.CQRs.UserProfile.Commands;
using Application.CQRs.UserProfile.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.UserProfile;

namespace WebApi.Controllers;

[ApiController]
[Route("api/userprofile")]
public class UserProfileController(IMediator mediator, IMapper mapper) : ControllerBase
{

    [HttpGet("get-profile/{userId}")]
    public async Task<ActionResult> GetUserProfile(int userId)
    {
        var query = new GetUserProfileQuery(userId);
        var dto = await mediator.Send(query);
        if (dto != null)
            return Ok(mapper.Map<UserProfileView>(dto));
        return NotFound();
    }

    [HttpPost("create-profile/{userId}")]
    public async Task<ActionResult> CreateUserProfile([FromBody] UserProfileDto createUserProfileDto, int userId)
    {
        var command = new CreateUserProfileCommand(
            userId,
            createUserProfileDto.SkillLevel,
            createUserProfileDto.DataVolume
            );

        await mediator.Send(command);
        return Ok();
    }

    [HttpPost("edit-profile/{userId}")]
    public async Task<ActionResult> EditUserProfile([FromBody] UserProfileDto createUserProfileDto, int userId)
    {
        var command = new EditUserProfileCommand(
            userId,
            createUserProfileDto.SkillLevel,
            createUserProfileDto.DataVolume
            );

        await mediator.Send(command);
        return Ok();
    }
}
