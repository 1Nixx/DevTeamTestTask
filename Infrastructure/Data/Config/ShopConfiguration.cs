using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	internal class ShopConfiguration : IEntityTypeConfiguration<Shop>
	{
		public void Configure(EntityTypeBuilder<Shop> builder)
		{
			builder.Property(d => d.Name).HasMaxLength(50).IsRequired();
			builder.Property(d => d.Address).HasMaxLength(50).IsRequired();
			builder.HasMany(d => d.Products).WithOne();
		}
	}
}
