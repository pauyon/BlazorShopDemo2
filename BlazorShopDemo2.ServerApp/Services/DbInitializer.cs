using BlazorShopDemo2.DataAccess.Data;
using BlazorShopDemo2.ServerApp.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace BlazorShopDemo2.ServerApp.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(Common.Constants.RoleAdmin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Common.Constants.RoleAdmin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Common.Constants.RoleCustomer)).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                return;
            }

            IdentityUser user = new()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
            };

            _userManager.CreateAsync(user, "Admin123!").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, Common.Constants.RoleAdmin).GetAwaiter().GetResult(); ;
        }
    }
}