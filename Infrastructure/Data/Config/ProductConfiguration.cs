using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	internal class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(d => d.Name).HasMaxLength(50).IsRequired();
			builder.Property(d => d.Price).HasColumnType("Money").IsRequired();
			builder.Property(d => d.Color).HasMaxLength(6).IsRequired();
			builder.HasMany(d => d.Orders).WithMany(d => d.Products);
			builder.HasOne(d => d.Shop).WithMany(d => d.Products);
			builder.Property(d => d.IsDeleted).HasDefaultValue(false).IsRequired();
		}
	}
}
