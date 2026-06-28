using GymManagementSystem.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.PL.Seed
{
    // This class creates the starting Roles and the Admin user the first time
    // the application runs against an empty database. Without this, nobody could
    // log in and use the system because there would be no roles and no users at all.
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<GymUser>>();

            // 1) Make sure the roles used by [Authorize(Roles = "...")] in the controllers exist.
            string[] roles = { "SuperAdmin", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2) Make sure there is at least one Super Admin account to log in with.
            string adminEmail    = "admin@powerfitness.com";
            string adminPassword = "Admin@123";

            var existingUser = await userManager.FindByEmailAsync(adminEmail);
            if (existingUser is null)
            {
                var adminUser = new GymUser
                {
                    Name        = "Super Admin",
                    UserName    = adminEmail,
                    Email       = adminEmail,
                    PhoneNumber = "01000000000"
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "SuperAdmin");
                }
            }
        }
    }
}
