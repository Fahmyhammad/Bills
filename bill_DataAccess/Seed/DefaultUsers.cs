using bill_Entities.Const;
using bill_Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bill_DataAccess.Seed
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var defaultusers = new AppUser
            {
                Name = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Address = "Tala",
                City = "Tala"
            };
            var user = await userManager.FindByEmailAsync(defaultusers.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultusers, Constants.PasswordAdmin);
                await userManager.AddToRoleAsync(defaultusers, Constants.Admin);
            }
        }

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            var defaultUser = new AppUser
            {
                Name = "User",
                UserName = "user@gmail.com",
                Email = "user@gmail.com",
                EmailConfirmed = true,
                Address = "Tala",
                City = "Tala"
            };
            var users = await userManager.FindByEmailAsync(defaultUser.Email);
            if(users == null)
            {
                await userManager.CreateAsync(defaultUser,Constants.PasswordUser);
                await userManager.AddToRoleAsync(defaultUser, Constants.User);
            }
        }

        public static async Task SeedEditorAsync(UserManager<AppUser> userManager)
        {
            var defaultUser = new AppUser
            {
                Name = "Editor",
                UserName = "editor@gmail.com",
                Email = "editor@gmail.com",
                EmailConfirmed = true,
                Address = "Tala",
                City = "Tala"
            };
            var users = await userManager.FindByEmailAsync(defaultUser.Email);
            if(users == null)
            {
                await userManager.CreateAsync(defaultUser, Constants.PasswordEditor);
                await userManager.AddToRoleAsync(defaultUser, Constants.Editor);
            }
        }
    }
}
