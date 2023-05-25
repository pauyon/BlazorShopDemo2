using Microsoft.AspNetCore.Identity;

namespace BlazorShopDemo2.Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}