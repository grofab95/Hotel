using FluentAssertions;
using Hotel.Domain.Entities;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests.PriceRuleEnetityTests
{
    public class PriceCalculatorTests
    {
        private Reservation _reservation;
        private PriceCalculator _priceCalculator;

        public PriceCalculatorTests()
        {
            _reservation = FakeReservationCreator.Get();
            _priceCalculator = new PriceCalculator(new List<PriceRule>
            {
                new PriceRule(RuleName.PriceWhenChild, RuleType.DecreasingByPercentage, "Reguła dla dzieci", 50, 1, true),
                new PriceRule(RuleName.PriceWhenNewlywed, RuleType.DecreasingByPercentage, "Reguła dla nowożeńców", 100, 2, false),
                new PriceRule(RuleName.PriceWhenBreakfest, RuleType.IncreasingByValue, "Reguła dla śniadań", 10, 3, true)
            });
        }

        [Theory]
        [InlineData(0, 170)]
        [InlineData(1, 0)] 
        [InlineData(2, 120)]
        [InlineData(3, 20)] // ustalone ceny
        public void CalculateRoomPrice_Expected_CalculatedRoomPrice(int roomId, decimal expectedPrice)
        {
            var reservationRoom = _reservation.ReservationRooms[roomId];
            var actual = _priceCalculator.CalculateRoomPrice(reservationRoom);

            actual.Should().Be(expectedPrice);
        }

        [Fact]
        public void CalculateReservationPrice_Excpected_CalculatedReservationPrice()
        {
            var actual = _reservation.GetCalculatedPrice(_priceCalculator);
            var expected = 310;

            actual.Should().Be(expected);
        }
    }
}
