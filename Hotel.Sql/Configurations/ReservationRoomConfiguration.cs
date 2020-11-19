using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Sql.Configurations
{
    class ReservationRoomConfiguration : IEntityTypeConfiguration<ReservationRoom>
    {
        public void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {
            builder.ToTable("ReservationRooms");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");

            //builder.HasOne(x => x.Reservation)
            //    .WithMany(x => x.ReservationRooms)
            //    .HasForeignKey(x => x.ReservationId);

            builder.HasMany(x => x.Guests).WithOne(x => x.ReservationRoom).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
