using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
	public class IdentitySeed
    {
		public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (roleManager.Roles.Any())
				return;

			await roleManager.CreateAsync(new IdentityRole
			{
				Name = "Admin"
			});
			await roleManager.CreateAsync(new IdentityRole
			{
				Name = "Courier"
			});

			var admin = new IdentityUser
			{
				UserName = "admin@gmail.com",
				Email = "admin@gmail.com"
			};
			var courier = new IdentityUser
			{
				UserName = "courier@gmail.com",
				Email = "courier@gmail.com"
			};

			await userManager.CreateAsync(admin, "Test*1234");
			await userManager.AddToRoleAsync(admin, "Admin");
			
			await userManager.CreateAsync(courier, "Test*1234");
			await userManager.AddToRoleAsync(courier, "Courier");
		}

	}
}
