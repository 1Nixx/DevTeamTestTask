using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Web.Filters;

namespace Web.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IShopService, ShopService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IProductService, ProductService>();

			return services;
		}
	}
}
