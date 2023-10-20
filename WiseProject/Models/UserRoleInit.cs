using Microsoft.AspNetCore.Identity;
using WiseProject.Data;

namespace WiseProject.Models
{
    public class UserRoleInit
    {
        public static async Task InitAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
            var context = service.GetRequiredService<ApplicationDbContext>();

            string[] roleNames = { "Admin", "User", "Instructor" };

            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var email = "admin@mycompany.com";
            var pass = "Pa$$w0rd";


            //var ad = userManager.FindByEmailAsync(email).Result;
            //userManager.AddToRoleAsync(ad, "Admin").Wait();

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                IdentityUser admin = new()
                {
                    Email = email,
                    UserName = email
                };
                IdentityResult result = await userManager.CreateAsync(admin, pass);
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "Admin").Wait();
                }
            }

        }
    }
}
