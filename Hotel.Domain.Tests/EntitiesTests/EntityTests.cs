using FluentAssertions;
using Hotel.Domain.Entities;
using Xunit;

namespace Hotel.Domain.Tests.EntitiesTests
{
    public class EntityTests
    {
        private Area _area1 = new Area("area1")
        {
            Id = 1
        };

        private Area _area2 = new Area("area2")
        {
            Id = 2
        };

        private Area _area3 = new Area("area3")
        {
            Id = 1
        };


        [Fact]
        public void Given_False_When_NotEqul()
        {

            var actual = _area1 == _area2;

            actual.Should().BeFalse();
        }

        [Fact]
        public void Given_EqualOperator2_When_Then()
        {

            var actual = _area1 == null;

            actual.Should().BeFalse();
        }

        [Fact]
        public void Given_EqualOperator3_When_Then()
        {

            var actual = null == _area2;

            actual.Should().BeFalse();
        }

        [Fact]
        public void Given_EqualOperator5_When_Then()
        {

            var actual = _area1 == _area3;

            actual.Should().BeTrue();
        }

        [Fact]
        public void Given_EqualOperator6_When_Then()
        {

            var area11 = _area1;
            var actual = _area1 == area11;

            actual.Should().BeTrue();
        }
    }
}
