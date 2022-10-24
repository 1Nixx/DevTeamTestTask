using Core.Interfaces;
using Infrastructure.Data;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using React.AspNet;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<MarketContext>(options =>
	options.UseSqlServer(connectionString));
services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
services.AddReact();
services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
services.AddControllersWithViews();

var app = builder.Build();

await SeedDatabaseAsync();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseReact(config => { });
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
