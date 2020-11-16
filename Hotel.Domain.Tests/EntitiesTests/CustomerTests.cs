using FluentAssertions;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests
{
    public class CustomerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void ValidFirstName_When_FirstNameNotExist_Should_Throw_MissingValueException(string firstName)
        {
            FluentActions.Invoking(() => new Customer(firstName, "Kowalski"))
                .Should()
                .Throw<MissingValueException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void ValidLastName_When_LastNameNotExist_Should_Throw_MissingValueException(string lastName)
        {
            FluentActions.Invoking(() => new Customer("Jan", lastName))
                .Should()
                .Throw<MissingValueException>();
        }

        [Fact]
        public void ValidDifferentNames_When_FirstNameEqualLastName_Should_Throw_HotelException()
        {
            FluentActions.Invoking(() => new Customer("Jan", "Jan"))
                .Should()
                .Throw<HotelException>();
        }

        [Fact]
        public void CustomerToString_Should_Return_FirstNameAndLastName()
        {
            var customer = new Customer("Jan", "Kowalski");

            customer.ToString().Should().Be("Jan Kowalski");
        }
    }
}
