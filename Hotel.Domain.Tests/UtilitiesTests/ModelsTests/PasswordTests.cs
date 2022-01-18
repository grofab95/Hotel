using FluentAssertions;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Utilities.Models;
using Xunit;

namespace Hotel.Domain.Tests.UtilitiesTests.ModelsTests;

public class PasswordTests
{
    private const string _password = "kot123";

    [Theory]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("")]
    public void ValidValue_When_ValueNotExist_Should_Throw_MissingValueException(string password)
    {
        FluentActions.Invoking(() => new Password(password))
            .Should()
            .Throw<MissingValueException>();
    }

    [Fact]
    public void Password_Should_Has_Hash()
    {
        var password = new Password(_password);
        password.PasswordHash.Should().NotBeEmpty();
    }

    [Fact]
    public void Password_Should_Has_Salt()
    {
        var password = new Password(_password);
        password.PasswordSalt.Should().NotBeEmpty();
    }

    [Fact]
    public void Password_Should_Be_Hashed()
    {
        var password = new Password(_password);
        var password2 = new Password(password.PasswordHash, password.PasswordSalt);

        var result = password2.Equals(_password);
        result.Should().BeTrue();
    }
}