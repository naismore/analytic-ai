using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base()
        {
        }

        // Конструктор с именем роли
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}