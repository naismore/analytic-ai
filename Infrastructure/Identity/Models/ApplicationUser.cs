using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser<int>
{
    public int BusinessUserId { get; set; }

}