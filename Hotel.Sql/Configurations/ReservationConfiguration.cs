using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Sql.Configurations;

class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()");

        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Reservations)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasMany(x => x.ReservationRooms)
            .WithOne(x => x.Reservation)
            .OnDelete(DeleteBehavior.Cascade);
    }
}