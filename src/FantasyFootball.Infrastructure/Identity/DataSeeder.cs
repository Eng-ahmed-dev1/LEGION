namespace FantasyFootball.Infrastructure.Identity;

public static class DataSeeder
{
    public static async Task SeedRolesAndAdminAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        // 1. Seed Roles
        var roles = new[] { "Admin", "User", "Manager" };
        
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }

        // 2. Seed Default Admin User
        var adminEmail = "admin@fantasy.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        
        if (adminUser == null)
        {
            var newAdmin = new ApplicationUser
            {
                UserName = "SuperAdmin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@123456!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}
