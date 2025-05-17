
using eStoreCA.Domain.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Shared.Common;

namespace eStoreCA.Infrastructure.Data.Initializer
{
    public class UserInitializer
    {
        public static void AddUser(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

            if (!db.Roles.Any(r => r.Name == "Administrator"))
            {

                roleManager.CreateAsync(new ApplicationRole() { Name = "Administrator" }).GetAwaiter().GetResult();

            }

            if (!db.Roles.Any(r => r.Name == "User"))
            {

                roleManager.CreateAsync(new ApplicationRole() { Name = "User" }).GetAwaiter().GetResult();

            }


            if (!db.ApplicationUsers.Any(u => u.Email == "admin@gmail.com"))
            {
                userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Belal",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    FullName = "Belal Badawy",
                    CreatedDate = DateTime.Now

                }, "Admin123$").GetAwaiter().GetResult();
            }

            ApplicationUser user = db.ApplicationUsers.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();

            var newRoleName = "Administrator";

            if (!db.Roles.Any(r => r.Name == "Administrator"))
            {

                roleManager.CreateAsync(new ApplicationRole() { Name = "Administrator" }).GetAwaiter().GetResult();

            }

            var newRole = roleManager.FindByNameAsync(newRoleName).GetAwaiter().GetResult();

            if (newRole != null)
            {
                List<AppClaim> existsAppClaims = new List<AppClaim>();

                existsAppClaims = db.AppClaims.ToListAsync().GetAwaiter().GetResult();

                var claims = roleManager.GetClaimsAsync(newRole).GetAwaiter().GetResult();

                foreach (var ca in existsAppClaims)
                {
                    if (!string.IsNullOrEmpty(ca.ClaimTitle))
                    {
                        if (!claims.Any(o => o.Value.ToUpper() == ca.ClaimTitle.ToUpper()))
                        {
                            roleManager.AddClaimAsync(newRole, new Claim("permission", ca.ClaimTitle.ToUpper()))
                                .GetAwaiter().GetResult();
                        }
                    }
                }

            }

            if (user != null && !string.IsNullOrEmpty(newRoleName))
            {
                userManager.AddToRoleAsync(user, newRoleName).GetAwaiter().GetResult();
            }

            #region Custom
            #endregion Custom

        }
    }
}
