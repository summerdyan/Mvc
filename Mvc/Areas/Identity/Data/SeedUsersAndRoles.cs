// creates some users with custom roles
using Microsoft.AspNetCore.Identity;
using Mvc.Areas.Identity.Data;

namespace Mvc.Areas.Identity.Data
{
    public static class SeedUsersAndRoles
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // managers
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        // create a couple users
        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("jd@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "jd@localhost";
                user.Email = "jd@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    // role = manager
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }


            if (userManager.FindByEmailAsync("alex@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "alex@localhost";
                user.Email = "alex@localhost";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    // role = admin
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        // create some custom roles
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // manager role (edit permissions normally)
            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Manager";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            // admin role (create, edit, delete permissions normally)
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
