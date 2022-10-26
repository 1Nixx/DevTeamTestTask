using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services.AddAutoMapper(typeof(MappingProfiles));

services.AddDbContext<MarketContext>(options =>
	options.UseSqlServer(connectionString));

services.AddDbContext<IdentityDbContext>(options =>
	options.UseSqlServer(connectionString));

services.AddIdentity<IdentityUser, IdentityRole>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.SignIn.RequireConfirmedEmail = false;
		options.SignIn.RequireConfirmedPhoneNumber = false;
	})
	.AddEntityFrameworkStores<IdentityDbContext>()
	.AddSignInManager<SignInManager<IdentityUser>>();

services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddReact();
services.AddSwaggerGen(); 
services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();

services.AddMvc();

var app = builder.Build();

await SeedDatabaseAsync();
await SeedIdentityAsync();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseReact(config => {});
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


async Task SeedDatabaseAsync()
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<MarketContext>();
	await context.Database.MigrateAsync();
	await MarketContextSeed.SeedAsync(context);
}

async Task SeedIdentityAsync()
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	var identityContext = services.GetRequiredService<IdentityDbContext>();
	await identityContext.Database.MigrateAsync();
	await IdentitySeed.SeedAsync(userManager, roleManager);
}
