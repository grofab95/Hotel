using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Application
{
    public class PriceCalculator
    {
        private RuleManager _ruleManager;
        private const decimal normalPrice = 50;

        public PriceCalculator(List<PriceRule> priceRules)
        {
            _ruleManager = new RuleManager(priceRules);
        }

        public decimal CalculatePriceForReservationRoom(ReservationRoom reservationRoom)
        {
            var calculatedPrice = normalPrice;

            if (reservationRoom.IsForNewlyweds)
            {
                var forNewlywedsRule = _ruleManager.GetRuleByName(RuleName.PriceWhenNewlyweds);
                calculatedPrice = forNewlywedsRule.GetCalculatedPrice(calculatedPrice);
            }

            if (reservationRoom.ChildrenAmount > 0)
            {
                var forChildrenRule = _ruleManager.GetRuleByName(RuleName.PriceWhenChildren);

                for (int i = 0; i < reservationRoom.ChildrenAmount; i++)
                    calculatedPrice = forChildrenRule.GetCalculatedPrice(calculatedPrice);
            }

            return calculatedPrice;
        }

        public decimal CalculateReservationPrice(Reservation reservation)
        {
            var reservationRoomsPrice = reservation.ReservationRooms
                .Sum(x => CalculatePriceForReservationRoom(x));

            if (reservation.WithBreakfest)
            {
                var forBreakfestRule = _ruleManager.GetRuleByName(RuleName.PriceWithBreakfest);
                var calculatedCalue = forBreakfestRule.GetCalculatedPrice(0); //

                reservationRoomsPrice += (reservation.PeopleAmount * calculatedCalue);
            }

            return reservationRoomsPrice;
        }
    }
}
