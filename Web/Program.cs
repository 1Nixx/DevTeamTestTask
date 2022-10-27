using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using React.AspNet;
using Web.Extensions;
using Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services.AddAutoMapper(typeof(MappingProfiles));

services.AddDbContext<MarketContext>(options =>
	options.UseSqlServer(connectionString));

services.AddDbContext<IdentityDbContext>(options =>
	options.UseSqlServer(connectionString));

services.AddIdentityServices();
services.AddApplicationServices();
services.AddReactServices();
services.AddSwaggerGen();
services.AddMvc();

var app = builder.Build();

await app.UseDatabaseSeed();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
else
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseReact(config => { });
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
