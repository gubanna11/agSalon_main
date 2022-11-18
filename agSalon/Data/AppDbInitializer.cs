using agSalon.Data.Static;
using agSalon.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Data
{
    public static class AppDbInitializer
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.Worker))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Worker));

                if (!await roleManager.RoleExistsAsync(UserRoles.Client))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Client));


                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Client>>();

                string adminEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if(adminUser == null)
                {
                    var newAdminUser = new Client()
                    {
                        Name = "Admin",
                        Surname = "Admin",
                        Phone = "+380662738199",
                        DateBirth = new DateTime(1990, 2, 3),
                        Email = adminEmail,
                        EmailConfirmed = true,
                        UserName = adminEmail
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
