using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.Extensions
{
	public static class SeedDatabaseExtensions
	{
		public static async Task<WebApplication> UseDatabaseSeed(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;

			await SeedDatabaseAsync(services);
			await SeedIdentityAsync(services);

			return app;
		}

		private static async Task SeedDatabaseAsync(IServiceProvider services)
		{
			var context = services.GetRequiredService<MarketContext>();
			await context.Database.MigrateAsync();

			await MarketContextSeed.SeedAsync(context);
		}

		private static async Task SeedIdentityAsync(IServiceProvider services)
		{
			var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

			var identityContext = services.GetRequiredService<IdentityDbContext>();
			await identityContext.Database.MigrateAsync();

			await IdentitySeed.SeedAsync(userManager, roleManager);
		}
	}
}
