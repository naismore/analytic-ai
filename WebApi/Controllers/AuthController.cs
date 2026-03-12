using Application.CQRs.Auth.Commands;
using Application.CQRs.Auth.Dto;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator, IMapper mapper) : ControllerBase
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
    public async Task<ActionResult<UserLoginView>> Login([FromBody] UserAuthDto userAuthDto)
    {
        var command = new LoginUserCommand(userAuthDto.Username, userAuthDto.Password);
        var result = await mediator.Send(command);

        Response.Cookies.Append(
        "refreshToken",
        result.RefreshToken,
        new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = result.RefreshTokenExpiresAt,
            Path = "/api/auth"
        });

        return Ok(mapper.Map<UserLoginView>(result));
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginDto>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var command = new RefreshTokenCommand(refreshToken);
        var result = await mediator.Send(command);

        Response.Cookies.Append(
        "refreshToken",
        result.RefreshToken,
        new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = result.RefreshTokenExpiresAt,
            Path = "/api/auth"
        });


        return Ok(result);
    }
}