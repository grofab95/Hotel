using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Sql.Configurations
{
    class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Area);
        }
    }
}
