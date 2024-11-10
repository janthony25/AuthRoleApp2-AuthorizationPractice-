using AuthRoleApp2.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthRoleApp2.Services
{
    public class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Create roles if they dont exist
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Client"))
                await roleManager.CreateAsync(new IdentityRole("Client"));

            // Ensure admin user exists
            var adminUser = await userManager.GetUsersInRoleAsync("Admin");
            if(adminUser.Count == 0)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    FirstName = "admin",
                    LastName = "user",
                    CreatedAt = DateTime.Now,
                    EmailConfirmed = true
                };

                string adminPassword = "J@nth0ny25";

                var result = await userManager.CreateAsync(admin, adminPassword);
                if(result.Succeeded)
                {
                    // Add the new user to the Admin role
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    // Log errors if user creation fails
                    foreach(var error in result.Errors)
                    {
                        Console.WriteLine($"Errors: {error.Description}");
                    }
                }
            }
        }
    }
}
