using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data;
using DataAccess.Data.Identity;

namespace RedMango_Api.Services
{
    public class SeedRoles
    {
        public static Task SeedUsersAndRoles(RoleManager<IdentityRole> roleManager,
           UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            if (!roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {

                roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                // if roles are not created, then we craate admin user as well

                string adminEmail = "admin@localhost.com";
                userManager.CreateAsync(new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    PhoneNumber = "123456789",
                    EmailConfirmed = true
                }, "Admin123!").GetAwaiter().GetResult();

                string userEmail = "user@localhost.com";
                userManager.CreateAsync(new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    PhoneNumber = "123456789",
                    EmailConfirmed = true
                }, "User123!").GetAwaiter().GetResult();

                ApplicationUser adminUser = db.Users.FirstOrDefault(u => u.Email == adminEmail);
                userManager.AddToRoleAsync(adminUser, SD.Role_Admin).GetAwaiter().GetResult();

                ApplicationUser user = db.Users.FirstOrDefault(u => u.Email == userEmail);
                userManager.AddToRoleAsync(user, SD.Role_Customer).GetAwaiter().GetResult();


            }

            return Task.CompletedTask;
        }
    }
}
