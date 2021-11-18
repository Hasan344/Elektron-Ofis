using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Identity
{
    public static class IdentitySeed
    {
        public static async Task Seed(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            var user = new IdentityUser
            {
               UserName=configuration["User:AdminUser:username"],
               Email= configuration["User:AdminUser:email"],
               EmailConfirmed=true
            };
            var role = configuration["User:AdminUser:role"];
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
               await roleManager.CreateAsync(new IdentityRole(role));
                var result = await userManager.CreateAsync(user, configuration["User:AdminUser:password"]);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,role);
                }
            }
        }
    }
}
