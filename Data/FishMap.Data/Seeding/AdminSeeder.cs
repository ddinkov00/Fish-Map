namespace FishMap.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FishMap.Common;
    using FishMap.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var adminEmail = "mitaka1971@gmail.com";

            if (dbContext.Users.Any())
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = new ApplicationUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
            };

            await userManager.CreateAsync(admin, "Mitaka1971!");
            await dbContext.SaveChangesAsync();
            await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
        }
    }
}
