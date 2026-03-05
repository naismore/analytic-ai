using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        //private UsersService _usersService;


        //[HttpPost("login")]
        //public async Task<IResult> Login([FromBody] UserLoginDTO userLoginDTO)
        //{
        //    ApplicationUser applicationUser = await _userManager.FindByEmailAsync(userLoginDTO.Email);
        //    bool checkPasswordResult = await _userManager.CheckPasswordAsync(applicationUser, userLoginDTO.Password);

        //    if (!checkPasswordResult || applicationUser == null)
        //    {
        //        return Results.Unauthorized();
        //    }


        //}

        /*public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("register", async (Request request, UserManager<ApplicationUser> userManager, DTADbContext dbContext) =>
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync();

                try
                {
                    var user = new User
                    {
                        UserStatus = UserStatus.Active,
                        CreatedAt = DateTime.UtcNow
                    };

                    var applicationUser = new ApplicationUser
                    {
                        Email = request.Email,
                        UserName = request.Email,
                        User = user
                    };

                    var userProfile = new UserProfile
                    {
                        User = user,
                        SkillLevel = SkillLevel.Junior,
                        DataVolume = 0,
                        LastUpdated = DateTime.UtcNow
                    };

                    user.ApplicationUser = applicationUser;
                    user.Profile = userProfile;

                    UserRepository userRepository = new UserRepository(dbContext);
                    UserProfileRepository userProfileRepository = new UserProfileRepository(dbContext);

                    await userRepository.InsertAsync(user);
                    await userProfileRepository.InsertAsync(userProfile);

                    IdentityResult createUserResult = await userManager.CreateAsync(applicationUser, request.Password);

                    if (!createUserResult.Succeeded)
                    {
                        return Results.BadRequest(createUserResult.Errors);
                    }

                    IdentityResult addToRoleResult = await userManager.AddToRoleAsync(applicationUser, UserRoles.Member);
                    if (!addToRoleResult.Succeeded)
                    {
                        return Results.BadRequest(addToRoleResult.Errors);
                    }

                    await dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return Results.Ok(new
                    {
                        UserId = user.Id,
                        ApplicationUserId = applicationUser.Id,
                        Email = applicationUser.Email
                    });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }*/
    }
}
