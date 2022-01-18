using FluentAssertions;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests;

public class AreaTests
{
    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("")]
    public void ValidIfNameExist_When_NotExist_Should_Throw_MissingValueException(string name)
    {
        FluentActions.Invoking(() => new Area(name))
            .Should()
            .Throw<MissingValueException>();
    }
}