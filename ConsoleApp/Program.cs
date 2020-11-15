﻿using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static HotelContext context = new HotelContext();

        static void Main()
        {
            //Refiller.Start();
            var priceRules = context.PriceRules.ToList();
            var priceCalculator = new PriceCalculator(priceRules);

            var reservation = context.Reservations.First();

            //var room1 = reservation.ReservationRooms[0];
            //var room2 = reservation.ReservationRooms[1];
            //var room3 = reservation.ReservationRooms[2];
            //var room4 = reservation.ReservationRooms[3];

            //var res1 = reservation.AddGuestToRoom(room1, "Wujek Andrzej", false, false, true);
            //var res2 = reservation.AddGuestToRoom(room1, "Ciocia Ania", false, false, true);
            //var res3 = reservation.AddGuestToRoom(room1, "Janusz", true, false, false);
            //var res4 = reservation.AddGuestToRoom(room1, "Zosia", true, false, false);

            //var res5 = reservation.AddGuestToRoom(room2, "Pani młoda", false, true, true);
            //var res6 = reservation.AddGuestToRoom(room2, "Pan młody", false, true, true);

            //var res7 = reservation.AddGuestToRoom(room3, "Jacek", false, false, true);
            //var res8 = reservation.AddGuestToRoom(room3, "Eliza", false, false, true);

            //var res9 = reservation.AddGuestToRoom(room4, "Babcia Agnieszka", false, false, false, 10);
            //var res0 = reservation.AddGuestToRoom(room4, "Dziadek Józef", false, false, false, 10);

            //context.SaveChanges();

            var totalPrice = reservation.GetCalculatedPrice(priceCalculator);
        }
    }
}