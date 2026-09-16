using CarRental.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CarRental.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = config;
        }
        public void Initialize()
        {
            if (_db.Database.GetPendingMigrations().Any())
            {
                _db.Database.Migrate();
            }

            if (!_roleManager.RoleExistsAsync(SD.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
            }
            string email = _configuration["AdminUser:Email"];
            string password = _configuration["AdminUser:Password"];
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Admin user email or password is not configured.");
            }

            var user = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var result = _userManager.CreateAsync(user, password).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }

            }

            if (!_userManager.IsInRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult())
            {
                _userManager.AddToRoleAsync(user, SD.AdminRole).GetAwaiter().GetResult();
            }
        }
    }
}
