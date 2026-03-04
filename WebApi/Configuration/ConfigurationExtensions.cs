//using Domain.Roles;
//using Infrastructure.Database;
//using Microsoft.AspNetCore.Identity;
////using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

//namespace WebApi.Configuration
//{
//    public static class ConfigurationExtensions
//    {
//        public static void ApplyMigrations(this WebApplication app)
//        {
//            using (var scope = app.Services.CreateScope())
//            {
//                var db = scope.ServiceProvider.GetRequiredService<DTADbContext>();
//                db.Database.Migrate();
//            }
//        }

//        public static async Task CreateIdentityRoles(this WebApplication app)
//        {
//            using (var scope = app.Services.CreateScope())
//            {
//                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
//                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
//                {
//                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//                }
//                if (!await roleManager.RoleExistsAsync(UserRoles.Member))
//                {
//                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Member));
//                }
//            }
//        }
//    }
//}
