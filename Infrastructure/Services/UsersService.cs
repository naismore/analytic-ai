//using Domain.DTO;
//using Domain.Entities;
//using Domain.Roles;
//using Infrastructure.Database;
//using Infrastructure.Repositories;
//using Microsoft.AspNetCore.Identity;

//namespace Infrastructure.Services
//{
//    public class UsersService
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly DTADbContext _dbContext;

//        public async Task Register(UserRegistrationDTO userRegistrationDTO)
//        {
//            using var transaction = await _dbContext.Database.BeginTransactionAsync();

//            try
//            {
//                var user = new User
//                {
//                    UserStatus = UserStatus.Active,
//                    CreatedAt = DateTime.UtcNow
//                };

//                var applicationUser = new ApplicationUser
//                {
//                    Email = userRegistrationDTO.Email,
//                    UserName = userRegistrationDTO.Email,
//                    User = user
//                };

//                var userProfile = new UserProfile
//                {
//                    User = user,
//                    SkillLevel = SkillLevel.Junior,
//                    DataVolume = 0,
//                    LastUpdated = DateTime.UtcNow
//                };

//                user.ApplicationUser = applicationUser;
//                user.Profile = userProfile;

//                UserRepository userRepository = new UserRepository(_dbContext);
//                UserProfileRepository userProfileRepository = new UserProfileRepository(_dbContext);

//                await userRepository.InsertAsync(user);
//                await userProfileRepository.InsertAsync(userProfile);

//                IdentityResult createUserResult = await _userManager.CreateAsync(applicationUser, userRegistrationDTO.Password);
//                if (!createUserResult.Succeeded)
//                {
//                    //return createUserResult;
//                }

//                IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(applicationUser, UserRoles.Member);
//                if (!addToRoleResult.Succeeded)
//                {
//                    //return addToRoleResult;
//                }

//                await _dbContext.SaveChangesAsync();

//                await transaction.CommitAsync();

//                //return IdentityResult.Success;
//            }
//            catch (Exception)
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }
//        }

//        public async Task Login(UserLoginDTO userLoginDTO)
//        {
//            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(userLoginDTO.Email);
//            bool checkPasswordResult = await _userManager.CheckPasswordAsync(applicationUser, userLoginDTO.Password);

//            if (!checkPasswordResult || applicationUser == null)
//            {
//                return Results.Unauthorized();
//            }
//        }
//    }
//}
