using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rad302feWebAPI2025.DataLayer;
using System.Threading.Tasks;


namespace Rad302feWebAPI2025
{
    public class DbSeeder
    {

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRolesAsync(roleManager);

            await SeedUserAsync(userManager);


        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task SeedUserAsync(RoleManager<IdentityRole> userManager)
        {
            var defaultAdmin = await userManager.AddToRoleAsync("admin@admin.com");
            if (defaultAdmin == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "Admin User"

                };

                var createAdminResult = await userManager.CreateAsync(defaultAdmin, "Admin@123");
                if (createAdminResult.Succeeded)
                {
                    await userManager.CreateAsync(defaultAdmin, "Admin");
                }
            }


            var defaultuser = await userManager.FindByEmailAsync("user@user.com");

            if (defaultAdmin == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    FirstName = "admin user",

                };

                var createAdminResult = await userManager.CreateAsync(defaultAdmin, "user@123");
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }
        }


    }
}

//needs fixing ill do ltr