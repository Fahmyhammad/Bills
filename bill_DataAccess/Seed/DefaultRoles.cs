using bill_Entities.Const;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_DataAccess.Seed
{
    public static class DefaultRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.Admin));
                await roleManager.CreateAsync(new IdentityRole(Constants.Editor));
                await roleManager.CreateAsync(new IdentityRole(Constants.User));
            }
        }
    }
}
