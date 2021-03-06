﻿// <auto-generated />
using System;
using Hotel.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hotel.Sql.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20210505193047_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Hotel.Domain.Entities.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<bool>("IsChild")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNewlyweds")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OrderedBreakfest")
                        .HasColumnType("bit");

                    b.Property<decimal>("PriceForStay")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ReservationRoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReservationRoomId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.PriceRuleEntity.PriceRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("ApplyNextRules")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("FriendlyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("RuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RuleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PriceRules");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.ReservationRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.HasIndex("RoomId");

                    b.ToTable("ReservationRooms");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeopleCapacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Views.ReservationInfoView", b =>
                {
                    b.Property<int>("BookingAmount")
                        .HasColumnType("int");

                    b.Property<int>("BreakfestAmount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOut")
                        .HasColumnType("datetime2");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<int>("RoomsAmount")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.ToView("ReservationInfoView");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Guest", b =>
                {
                    b.HasOne("Hotel.Domain.Entities.ReservationRoom", "ReservationRoom")
                        .WithMany("Guests")
                        .HasForeignKey("ReservationRoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ReservationRoom");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("Hotel.Domain.Entities.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.ReservationRoom", b =>
                {
                    b.HasOne("Hotel.Domain.Entities.Reservation", "Reservation")
                        .WithMany("ReservationRooms")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hotel.Domain.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("Reservation");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Room", b =>
                {
                    b.HasOne("Hotel.Domain.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Token", b =>
                {
                    b.HasOne("Hotel.Domain.Entities.User", "User")
                        .WithOne("Token")
                        .HasForeignKey("Hotel.Domain.Entities.Token", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.Reservation", b =>
                {
                    b.Navigation("ReservationRooms");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.ReservationRoom", b =>
                {
                    b.Navigation("Guests");
                });

            modelBuilder.Entity("Hotel.Domain.Entities.User", b =>
                {
                    b.Navigation("Token");
                });
#pragma warning restore 612, 618
        }
    }
}
