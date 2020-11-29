using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Refiller
    {
        public static void Start()
        {
            try
            {
                Refill();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Refill()
        {
            var context = new HotelContext();

            context.PriceRules.AddRange(new List<PriceRule>
            {
                new PriceRule(RuleName.PriceWhenChild, RuleType.DecreasingByPercentage, "Reguła dla dzieci", 50, 1, true),
                new PriceRule(RuleName.PriceWhenNewlywed, RuleType.DecreasingByPercentage, "Reguła dla nowożeńców", 100, 2, false),
                new PriceRule(RuleName.PriceWhenBreakfest, RuleType.IncreasingByValue, "Reguła dla śniadań", 10, 3, true)
            });

            var areas = new List<Area>
            {
                new Area("Budynek A"),
                new Area("Budynek B")
            };

            var rooms = new List<Room>
            {
                new Room(areas[0], "P1", 4),
                new Room(areas[0], "P2", 2),
                new Room(areas[0], "P3", 3),
                new Room(areas[1], "P4", 2),
                new Room(areas[1], "P5", 2),
                new Room(areas[1], "P6", 5)
            };

            context.Areas.AddRange(areas);
            context.Rooms.AddRange(rooms);

            var customers = new List<Customer>
            {
                new Customer("Monika", "Olejnik"),
                new Customer("Andrzej", "Kowalczyk"),
                new Customer("Anna", "Nowak"),
                new Customer("Stanisław", "Wodny"),
                new Customer("Maria", "Kaziewska"),
                new Customer("Stefan", "Gruby"),
                new Customer("Żaneta", "Kuźniar"),
                new Customer("Marian", "Nowy")
            };

            var checkIn = new DateTime(2020, 11, 20);
            var checkOut = new DateTime(2020, 11, 27);

            var reservation = Reservation.Create(customers[0], checkIn, checkOut);

            reservation.AddRoom(rooms[0]);
            reservation.AddRoom(rooms[1]);
            reservation.AddRoom(rooms[2]);
            reservation.AddRoom(rooms[3]);

            context.Reservations.Add(reservation);
            context.SaveChanges();
        }
    }
}
