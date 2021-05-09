using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel.Sql.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getdate()");
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasOne(x => x.Token)
                .WithOne(x => x.User)
                .HasForeignKey<Token>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
