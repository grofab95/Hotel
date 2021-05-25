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
        public void ValidFirstName_When_NameNotExist_Should_Throw_MissingValueException(string name)
        {
            FluentActions.Invoking(() => new Customer(name))
                .Should()
                .Throw<MissingValueException>();
        }
    }
}
