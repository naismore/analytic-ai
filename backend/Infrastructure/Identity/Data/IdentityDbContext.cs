using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Data
{
    // ApplicationUser - Базовая модель пользователя для Identity
    // IdentityRole<int> - Базовая модель ролей (дженерик от int - тип первичного ключа)
    // последний int - это тип первичного ключа для всех моделей
    public class AppIdentityDbContext : IdentityDbContext<
        ApplicationUser, 
        ApplicationRole, 
        int, 
        IdentityUserClaim<int>,       // TUserClaim
        IdentityUserRole<int>,        // TUserRole
        IdentityUserLogin<int>,       // TUserLogin
        IdentityRoleClaim<int>,       // TRoleClaim
        IdentityUserToken<int>>       // TUserToken>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("identity");

            builder.ConfigureIdentityEntities();
        }
    }
}
