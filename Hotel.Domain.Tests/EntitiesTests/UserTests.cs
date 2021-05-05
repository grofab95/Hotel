using FluentAssertions;
using Hotel.Domain.Entities;
using Hotel.Domain.Exceptions;
using Hotel.Domain.Utilities.Models;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests
{
    public class UserTests
    {
        private const string _name = "Tom";
        private const string _email = "tom@test.com";
        private readonly Password _password;

        public UserTests()
        {
            _password = new Password("kot123");
        }

        [Fact]
        public void CreatedUser_Should_Has_Name()
        {
            var user = new User(_name, _email, _password);
            var actual = user.Name;
            var expected = _name;

            actual.Should().Be(expected);
        }

        [Fact]
        public void CreatedUser_Should_Has_Email()
        {
            var user = new User(_name, _email, _password);
            var actual = user.Email;
            var expected = _email;

            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void ValidName_When_NameNotExist_Should_Throw_MissingValueException(string name)
        {
            FluentActions.Invoking(() => new User(name, _email, _password))
               .Should()
               .Throw<MissingValueException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("")]
        public void ValidEmail_When_EmailNotExist_Should_Throw_MissingValueException(string email)
        {
            FluentActions.Invoking(() => new User(_name, email, _password))
               .Should()
               .Throw<MissingValueException>();
        }

        [Theory]
        [InlineData("addresswp.pl")]
        [InlineData("addresswppl")]
        [InlineData("address#?")]
        [InlineData("4574568")]
        [InlineData("address@@wp.pl")]
        public void ValidEmail_For_InvalidFormat_Throw_InvalidEmail(string email)
        {
            FluentActions.Invoking(() => new User(_name, email, _password))
                .Should()
                .Throw<InvalidEmailException>();
        }
    }
}
