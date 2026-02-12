using Application.Users.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IMapper mapper, IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            var user = mediator.Send(new GetUserQuery(id));
            if (user.IsCompleted)
            {
                return Ok(mapper.Map<UserViewModel>(user));
            }
            return BadRequest();
        }
    }
}
