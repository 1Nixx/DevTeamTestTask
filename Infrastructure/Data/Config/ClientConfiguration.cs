using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
	internal class ClientConfiguration : IEntityTypeConfiguration<Client>
	{
		public void Configure(EntityTypeBuilder<Client> builder)
		{
			builder.Property(d => d.Name).HasMaxLength(50).IsRequired();
			builder.Property(d => d.Address).HasMaxLength(50).IsRequired();
			builder.HasMany(d => d.Orders).WithOne(d => d.Client);
		}
	}
}
