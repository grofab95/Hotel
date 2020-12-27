using FluentAssertions;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests
{
    public class ReservationTests
    {
        private FakeReservationCreator _fakeReservationCreator;

        private readonly DateTime _checkIn;
        private readonly DateTime _checkOut;

        public ReservationTests()
        {
            _checkIn = DateTime.Now.AddDays(1);
            _checkOut = DateTime.Now.AddDays(6);

            _fakeReservationCreator = new FakeReservationCreator(_checkIn, _checkOut);
        }

        [Fact]
        public void Create_Should_Return_Created_Reservation()
        {
            var customer = _fakeReservationCreator.GetCustomer();

            var reservation = Reservation.Create(customer, _checkIn, _checkOut);
            reservation.Should().NotBeNull();
        }

        [Fact]
        public void Create_When_CustomerIsNull_Should_Throw_MissingValueException()
        {
            FluentActions.Invoking(() => Reservation.Create(null, _checkIn, _checkOut))
               .Should()
               .Throw<MissingValueException>();
        }

        [Theory]
        [InlineData("2020, 5, 2", "2020, 5, 10")]
        public void ValidDates_For_DateInPast_Throw_HotelException(string checkInString, string checkOutString)
        {
            var customer = _fakeReservationCreator.GetCustomer();

            FluentActions.Invoking(() => Reservation.Create(customer, DateTime.Parse(checkInString), DateTime.Parse(checkOutString)))
               .Should()
               .Throw<HotelException>();
        }

        [Theory]
        [InlineData("2022, 5, 2", "2022, 5, 1")]
        public void ValidDates_For_CheckInLAterThanCheckOut_Throw_HotelException(string checkInString, string checkOutString)
        {
            var customer = _fakeReservationCreator.GetCustomer();

            FluentActions.Invoking(() => Reservation.Create(customer, DateTime.Parse(checkInString), DateTime.Parse(checkOutString)))
               .Should()
               .Throw<HotelException>();
        }

        [Fact]
        public void ChangeCheckIn_Should_SetCheckIn()
        {
            var reservation = _fakeReservationCreator.GetReservation();
            var newCheckIn = reservation.CheckIn.AddDays(1);

            var expected = newCheckIn;

            reservation.ChangeCheckIn(newCheckIn);
            var actual = reservation.CheckIn;

            actual.Should().Be(expected);
        }

        [Fact]
        public void ChangeCheckOut_Should_SetCheckOut()
        {
            var reservation = _fakeReservationCreator.GetReservation();
            var newCheckOut = reservation.CheckOut.AddDays(1);

            var expected = newCheckOut;

            reservation.ChangeCheckOut(newCheckOut);
            var actual = reservation.CheckOut;

            actual.Should().Be(expected);
        }

        [Fact]
        public void AddRoom_Should_AddReservationRoom()
        {
            var area = _fakeReservationCreator.GetAreas().First();
            var reservation = _fakeReservationCreator.GetReservation();
            var room = new Room(area, "Room XYZ", 5);

            var expected = room;
            reservation.AddRoom(room);
            var actual = reservation.ReservationRooms.LastOrDefault()?.Room;

            actual.Should().Be(expected);
        }

        [Fact]
        public void ValidAddedRoom_ForAlreadyExisting_Throw_HotelException()
        {
            var reservation = _fakeReservationCreator.GetReservation();
            var room = reservation.ReservationRooms.LastOrDefault()?.Room;

            FluentActions.Invoking(() => reservation.AddRoom(room))
               .Should()
               .Throw<HotelException>();
        }

        [Fact]
        public void DeleteRoom_Should_DeleteReservationRoom()
        {
            var reservation = _fakeReservationCreator.GetReservation();
            var room = reservation.ReservationRooms.First().Room;

            reservation.DeleteRoom(room);

            var deletedRoom = reservation.ReservationRooms.FirstOrDefault(x => x.Room == room);
            deletedRoom.Should().BeNull();
        }
    }
}
