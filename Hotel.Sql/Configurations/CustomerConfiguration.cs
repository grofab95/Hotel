using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Sql.Configurations;

class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

        builder.HasMany(x => x.Reservations)
            .WithOne(x => x.Customer)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}