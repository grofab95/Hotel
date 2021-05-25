using FluentAssertions;
using Hotel.Domain.Entities.PriceRuleEntity;
using Hotel.Domain.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests.PriceRuleEnetityTests
{
    public class PriceCalculatorTests
    {
        private PriceCalculator _priceCalculator;
        private FakeReservationCreator _creator;
        private static readonly decimal _priceForStay = 50;

        public PriceCalculatorTests()
        {
            _creator = new FakeReservationCreator();
            _priceCalculator = new PriceCalculator(new List<PriceRule>
            {
                new PriceRule(RuleName.PriceWhenChild, RuleType.DecreasingByPercentage, "Reguła dla dzieci", 50, 1, true),
                new PriceRule(RuleName.PriceWhenNewlywed, RuleType.DecreasingByPercentage, "Reguła dla nowożeńców", 100, 2, false),
                new PriceRule(RuleName.PriceWhenBreakfest, RuleType.IncreasingByValue, "Reguła dla śniadań", 10, 3, true)
            });
        }

        private GuestCreator CreateGuest()
        {
            var creator = new FakeReservationCreator().AddRoom();
            var reservation = creator.GetReservation();
            return new GuestCreator(reservation, _priceForStay);
        }

        [Fact]
        public void CalculateGuestPrice_WhenIsChild_Should_CalculateCorrectPrice()
        {
            //Arrange
            var guest = CreateGuest()
                .MarkAsChild()
                .GetGuest();

            //Act            
            var actual = _priceCalculator.CalculateGuestPrice(guest);
            var expected = 25m;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateGuestPrice_WhenIsNewlywed_Should_CalculateCorrectPrice()
        {
            //Arrange
            var guest = CreateGuest()
                .MarkAsNewlyweds()
                .GetGuest();

            //Act            
            var actual = _priceCalculator.CalculateGuestPrice(guest);
            var expected = 0m;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateGuestPrice_WhenIsChildAndHasBreakfest_Should_CalculateCorrectPrice()
        {
            //Arrange
            var guest = CreateGuest()
                .MarkAsChild()
                .MarkAsHavingOrderedBreakfest()
                .GetGuest();

            //Act            
            var actual = _priceCalculator.CalculateGuestPrice(guest);
            var expected = 35m;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateGuestPrice_WhenIsNewlywedAndHasBreakfest_Should_CalculateCorrectPrice()
        {
            //Arrange
            var guest = CreateGuest()
                .MarkAsNewlyweds()
                .MarkAsHavingOrderedBreakfest()
                .GetGuest();

            //Act            
            var actual = _priceCalculator.CalculateGuestPrice(guest);
            var expected = 0m;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateGuestPrice_WhenSettedPrice_Should_ReturnSettedPrice()
        {
            //Arrange
            var settedPrice = 634.99m;
            var guest = CreateGuest()
                .MarkAsNewlyweds()
                .SetPriceForStay(settedPrice)
                .MarkAsHavingOrderedBreakfest()
                .GetGuest();

            //Act            
            var actual = _priceCalculator.CalculateGuestPrice(guest);
            var expected = settedPrice;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateRoomPrice_Should_CalculateCorrectPrice()
        {
            //Arrange
            var creator = new FakeReservationCreator().AddRoom().AddGuest().AddGuest();           
            var reservationRoom = creator.GetReservation().ReservationRooms.First();

            //Act            
            var actual = _priceCalculator.CalculateRoomPrice(reservationRoom);
            var expected = 100m;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void CalculateReservationPrice_Should_CalculateCorrectPrice()
        {
            //Arrange
            var reservation = new FakeReservationCreator()
                .AddRoom()
                .AddGuest()
                .AddGuest()
                .AddRoom()
                .AddGuest()
                .GetReservation();

            //Act            
            var actual = _priceCalculator.CalculateReservationPrice(reservation);
            var expected = 150m;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
