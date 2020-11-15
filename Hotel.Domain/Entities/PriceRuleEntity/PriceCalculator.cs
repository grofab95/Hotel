using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities.PriceRuleEntity
{
    public class PriceCalculator
    {
        private RuleManager _ruleManager;
        private const decimal normalPrice = 50;

        public PriceCalculator(List<PriceRule> priceRules)
        {
            _ruleManager = new RuleManager(priceRules);
        }

        private decimal CalculatedGuestPrice(RoomGuest roomGuest)
        {
            if (roomGuest.PriceForStay != null)
                return roomGuest.PriceForStay.Value;

            var guestPrice = normalPrice;

            foreach (var priceRule in _ruleManager.GetOrderedRules())
            {
                if (!priceRule.IsRuleObligatoring(roomGuest))
                    continue;

                guestPrice = _ruleManager.GetCalculatedPrice(priceRule.RuleName, guestPrice);
            }

            return Math.Max(guestPrice, 0);
        }

        public decimal CalculateRoomPrice(ReservationRoom reservationRoom)
        {
            var roomPrice = reservationRoom.RoomGuests.Sum(x => CalculatedGuestPrice(x));

            return roomPrice;
        }

        public decimal CalculateReservationPrice(Reservation reservation)
        {
            var reservationRoomsPrice = reservation.ReservationRooms.Sum(x => CalculateRoomPrice(x));

            return Math.Round(reservationRoomsPrice, 2);
        }
    }
}
