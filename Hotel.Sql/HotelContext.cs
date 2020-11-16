using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Sql
{
    public class HotelContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<RoomGuest> RoomGuests { get; set; }
        public DbSet<PriceRule> PriceRules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-HV06FGL;Database=HOTEL;User Id=sa; Password=Q1w2e3;");

            //if (Config.Get.IsTestMode())
            //{
            optionsBuilder.UseLazyLoadingProxies();
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseLoggerFactory(DbLogger.Factory);
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new PriceRuleConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationRoomConfiguration());
            modelBuilder.ApplyConfiguration(new RoomGuestConfiguration());
        }
    }
}
