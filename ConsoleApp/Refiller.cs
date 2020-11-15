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
                new PriceRule(RuleName.PriceWhenChild, RuleType.DecreasingByPercentage, "Reguła dla dzieci", 50, 1),
                new PriceRule(RuleName.PriceWhenNewlywed, RuleType.DecreasingByPercentage, "Reguła dla nowożeńców", 100, 2),
                new PriceRule(RuleName.PriceWhenBreakfest, RuleType.IncreasingByValue, "Reguła dla śniadań", 10, 3)
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

            var customers = new List<Customer>
            {
                new Customer("Monika", "Olejnik"),
                new Customer("Andrzej", "Kowalczyk"),
                new Customer("Anna", "Nowak")
            };

            var checkIn = new DateTime(2020, 11, 20);
            var checkOut = new DateTime(2020, 11, 27);

            var createReservationResult = Reservation.CreateReservation(customers[0], checkIn, checkOut);
            if (createReservationResult.IsError)
                throw new Exception(createReservationResult.Message);

            var reservation = createReservationResult.Value;

            var res1 = reservation.AddReservationRoom(rooms[0]);
            var res2 = reservation.AddReservationRoom(rooms[1]);
            var res3 = reservation.AddReservationRoom(rooms[2]);
            var res4 = reservation.AddReservationRoom(rooms[3]);

            context.Reservations.Add(reservation);
            context.SaveChanges();
        }
    }
}
