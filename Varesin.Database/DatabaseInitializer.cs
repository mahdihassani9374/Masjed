using Microsoft.AspNetCore.Identity;
using System;
using Varesin.Database.Identity.Entities;
using Varesin.Domain.Enumeration;

namespace Varesin.Database
{
    public class DatabaseInitializer
    {

        public void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetValues(typeof(AccessCode));

            foreach (var role in roles)
            {

                if (!roleManager.RoleExistsAsync(role.ToString()).Result)
                {
                    var createRoleResult = roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString()
                    }).Result;
                }
            }

            var mobinUser = userManager.FindByNameAsync("09197442364").Result;
            var mahdiUser = userManager.FindByNameAsync("09212651629").Result;
            if (mobinUser == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09197442364",
                    PhoneNumber = "09197442364",
                    FullName = "مبین حسنی",
                    Gender = GenderType.Man,
                    IsSuperAdmin = true,
                    RegisterDate = DateTime.Now,
                }, "9197442364"
                 ).Result;
            }
            if (mahdiUser == null)
            {
                var result = userManager.CreateAsync(new User
                {
                    UserName = "09212651629",
                    PhoneNumber = "09212651629",
                    FullName = "مهدی حسنی",
                    Gender = GenderType.Man,
                    IsSuperAdmin = true,
                    RegisterDate = DateTime.Now,
                }, "9197572162"
                 ).Result;
            }
        }
    }
}
