using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Web.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{
			services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
			})
				.AddEntityFrameworkStores<IdentityDbContext>()
				.AddSignInManager<SignInManager<IdentityUser>>();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				 .AddCookie(options =>
				 {
					 options.LoginPath = "/Account/Login";
				 });

			return services;
		}
	}
}
