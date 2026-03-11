using Application.CQRs.Auth.Commands;
using Application.CQRs.Auth.Dto;
using Application.CQRs.Identity.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IResult> Register([FromBody] UserAuthDto userDto)
    {
        var command = new RegisterUserCommand(userDto.Username, userDto.Password);
        var dto = await mediator.Send(command);

        return Results.Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> Login([FromBody] UserAuthDto userAuthDto)
    {
        var command = new LoginUserCommand(userAuthDto.Username, userAuthDto.Password);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var command = new RefreshTokenCommand(refreshTokenDto.RefreshToken);
        var result = await mediator.Send(command);
        return Ok(result);
    }
}