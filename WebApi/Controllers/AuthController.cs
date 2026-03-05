using Application.CQRs.Auth.Commands;
using Application.CQRs.Auth.Dto;
using Application.CQRs.Identity.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] UserAuthDto userDto)
    {
        var command = new RegisterUserCommand(userDto.Username, userDto.Password);
        var dto = await mediator.Send(command);

        return Results.Ok();
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> Login([FromBody] UserAuthDto userAuthDto)
    {
        var command = new LoginUserCommand(userAuthDto.Username, userAuthDto.Password);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var command = new RefreshTokenCommand(refreshTokenDto.RefreshToken);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    //[HttpPost("logout")]
    //[Authorize]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<IActionResult> Logout(LogoutCommand command)
    //{
    //    // Получаем userId из токена
    //    command.UserId = int.Parse(User.FindFirst("user_id")?.Value ?? "0");

    //    // Получаем refresh token из запроса или куки
    //    command.RefreshToken = command.RefreshToken ??
    //        Request.Headers["X-Refresh-Token"].FirstOrDefault();

    //    var result = await _mediator.Send(command);
    //    return result ? Ok() : BadRequest();
    //}

    //[HttpPost("logout-all")]
    //[Authorize]
    //public async Task<IActionResult> LogoutAllDevices()
    //{
    //    var command = new LogoutCommand
    //    {
    //        UserId = int.Parse(User.FindFirst("user_id")?.Value ?? "0"),
    //        LogoutAllDevices = true
    //    };

    //    var result = await _mediator.Send(command);
    //    return result ? Ok() : BadRequest();
    //}
}