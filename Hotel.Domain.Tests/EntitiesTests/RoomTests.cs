using FluentAssertions;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests
{
    public class RoomTests
    {
        private readonly Area _area;

        public RoomTests()
        {
            _area = new Area("Budynek A");
        }

        [Fact]
        public void ValidIfAreaExist_When_NotExist_Should_Throw_MissingValueException()
        {
            FluentActions.Invoking(() => new Room(null, "p1", 1))
               .Should()
               .Throw<MissingValueException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void ValidIfNameExist_When_NotExist_Should_Throw_MissingValueException(string name)
        {
            FluentActions.Invoking(() => new Room(_area, name, 1))
               .Should()
               .Throw<MissingValueException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ValidIfPeopleCapacityIsPositive_When_Not_Should_Throw_MissingValueException(int peopleCapacity)
        {
            FluentActions.Invoking(() => new Room(_area, "p1", peopleCapacity))
               .Should()
               .Throw<HotelException>();
        }

        [Fact]
        public void RoomToString_Should_Return_AreAndRoomName()
        {
            var room = new Room(_area, "p1", 2);

            room.ToString().Should().Be($"{_area.Name} pokój p1");
        }
    }
}
