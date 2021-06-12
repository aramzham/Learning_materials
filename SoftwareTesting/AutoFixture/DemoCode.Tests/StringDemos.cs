using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class StringDemos
    {
        [Fact]
        public void BasicStrings()
        {
            // arrange
            var fixture = new Fixture();
            var sut = new NameJoiner();

            var firstName = fixture.Create("First_"); // from AutoFixture.SeedExtensions
            var lastName = fixture.Create("Last_");

            // act
            var result = sut.Join(firstName, lastName);

            // assert
            Assert.Equal(firstName + ' ' + lastName, result);
        }

        [Fact]
        public void Chars()
        {
            var fixture = new Fixture();
            var anonChar = fixture.Create<char>();
        }
    }
}