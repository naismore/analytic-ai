using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {  
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
