﻿using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Entities.Views;
using Hotel.Domain.Environment;
using Hotel.Domain.Exceptions;
using Hotel.Sql.Configurations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hotel.Sql;

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
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Config.Get.SqlConnection);

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
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TokenConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                var trackedEntries = ChangeTracker.Entries().ToList();
                trackedEntries.ForEach(x => x.State = EntityState.Detached);
            }
            return result;
        }
        catch (SqlException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            if (ex.InnerException?.Message?.Contains("Cannot insert duplicate key row in object") ?? false)
            {
                var error = ex.InnerException.Message;
                var value = error.Split(new string[] { "The duplicate key value is (", ")." }, StringSplitOptions.TrimEntries)[1];
                throw new HotelException($"Wartość {value.Replace(",", "")} już istnieje w bazie.");
            }
            throw;
        }
    }
}