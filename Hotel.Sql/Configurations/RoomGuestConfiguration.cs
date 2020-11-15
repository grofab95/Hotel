using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Sql.Configurations
{
    class RoomGuestConfiguration : IEntityTypeConfiguration<RoomGuest>
    {
        public void Configure(EntityTypeBuilder<RoomGuest> builder)
        {
            builder.ToTable("RoomGuests");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
