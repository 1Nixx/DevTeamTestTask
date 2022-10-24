using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
	public class MarketContext : DbContext
	{
		public MarketContext(DbContextOptions<MarketContext> options) : base(options)
		{
		}
		public DbSet<Shop> Clients { get; set; }
		public DbSet<Shop> Orders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Shop> Shops { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
