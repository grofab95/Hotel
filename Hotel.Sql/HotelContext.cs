using Hotel.Domain.Entities;
using Hotel.Domain.Entities.Common;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Entities.Views;
using Hotel.Sql.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Sql
{
    public class HotelContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<PriceRule> PriceRules { get; set; }
        public DbSet<ReservationInfoView> ReservationInfoViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=DESKTOP-HV06FGL;Database=HOTEL;User Id=sa; Password=Q1w2e3;");
            optionsBuilder.UseSqlServer("Server=192.168.0.10\\SQL2019;Database=HOTEL;User Id=sa; Password=Q1w2e3;");

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(DbLogger.Factory);            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyViews();

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new PriceRuleConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationRoomConfiguration());
            modelBuilder.ApplyConfiguration(new GuestConfiguration());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var res = await base.SaveChangesAsync(cancellationToken);

            if (res > 0)
            {
                var trackedEntries = ChangeTracker.Entries()
                    .ToList();

                trackedEntries.ForEach(x => x.State = EntityState.Detached);
            }

            return res;
        }
    }
}
