using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SFMB.DAL.Entities;

namespace SFMB.DAL
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create roles if they don't exist
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create Admin user
            var adminEmail = "admin@sfmb.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Create first regular user
            var user1Email = "user1@sfmb.com";
            var user1 = await userManager.FindByEmailAsync(user1Email);
            if (user1 == null)
            {
                user1 = new ApplicationUser
                {
                    Email = user1Email,
                    UserName = user1Email,
                    FirstName = "John",
                    LastName = "Doe",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user1, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "User");
                }
            }

            // Create second regular user
            var user2Email = "user2@sfmb.com";
            var user2 = await userManager.FindByEmailAsync(user2Email);
            if (user2 == null)
            {
                user2 = new ApplicationUser
                {
                    Email = user2Email,
                    UserName = user2Email,
                    FirstName = "Jane",
                    LastName = "Smith",
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user2, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user2, "User");
                }
            }
        }
    }
}
