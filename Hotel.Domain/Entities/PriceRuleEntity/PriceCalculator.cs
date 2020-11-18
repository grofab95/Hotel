using Hotel.Domain.Environment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Domain.Entities.PriceRuleEntity
{
    public class PriceCalculator
    {
        private PriceRuleManager _ruleManager;
        private decimal _standartPriceForStay;

        public PriceCalculator(List<PriceRule> priceRules)
        {
            _ruleManager = new PriceRuleManager(priceRules);
            _standartPriceForStay = Config.Get.PriceForStay;
        }

        public decimal CalculateGuestPrice(Guest guest)
        {
            if (guest.PriceForStay != _standartPriceForStay)
                return guest.PriceForStay;

            var guestPrice = _standartPriceForStay;

            foreach (var priceRule in _ruleManager.GetOrderedRules())
            {
                if (!priceRule.IsRuleObligatoring(guest))
                    continue;

                guestPrice = _ruleManager.GetCalculatedPrice(priceRule.RuleName, guestPrice);

                if (!priceRule.ApplyNextRules)
                    break;
            }

            return Math.Max(guestPrice, 0);
        }

        public decimal CalculateRoomPrice(ReservationRoom reservationRoom)
        {
            var roomPrice = reservationRoom.RoomGuests.Sum(x => CalculateGuestPrice(x));

            return roomPrice;
        }

        public decimal CalculateReservationPrice(Reservation reservation)
        {
            var reservationRoomsPrice = reservation.ReservationRooms.Sum(x => CalculateRoomPrice(x));

            return Math.Round(reservationRoomsPrice, 2);
        }
    }
}
