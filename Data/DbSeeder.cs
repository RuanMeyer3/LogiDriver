using LogiDriverPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection; 
using System;
using System.Threading.Tasks;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();

        string email = "supervisor@logidriver.com";
        string password = "Password123";

        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new User
            {
                UserName = email,
                Email = email,
                FullName = "System Supervisor",
                Role = "Supervisor",
                Status = "Active",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, password);
        }
    }
}
