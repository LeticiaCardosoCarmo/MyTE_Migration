using Microsoft.AspNetCore.Identity;
using MyTE_Migration.Context;
using System.Runtime.InteropServices;

namespace MyTE_Migration.Services

{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
      
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Criação dos perfis e dos usuários 
        public async Task SeedRolesAsync()
        {
            if (!await roleManager.RoleExistsAsync("User"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                IdentityResult roleResult = await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                IdentityResult roleRresult = await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync("Gerente"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Gerente",
                    NormalizedName = "GERENTE",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                IdentityResult roleRresult = await roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUsersAsync()
        {
            if (await userManager.FindByEmailAsync("usuario@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await userManager.CreateAsync(user, "Numsey#2024");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }

            if (await userManager.FindByEmailAsync("admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await userManager.CreateAsync(user, "Numsey#2024");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }

            if (await userManager.FindByEmailAsync("gerente@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "gerente@localhost";
                user.Email = "gerente@ocalhost";
                user.NormalizedUserName = "GERENTE@LOCALHOST";
                user.NormalizedEmail = "GERENTE@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await userManager.CreateAsync(user, "Numsey#2024");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
}
