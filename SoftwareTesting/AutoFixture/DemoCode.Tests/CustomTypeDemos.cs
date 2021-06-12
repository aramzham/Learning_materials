using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class CustomTypeDemos
    {
        [Fact]
        public void ManualCreation()
        {
            // arrange
            var sut = new EmailMessageBuffer();

            var message = new EmailMessage("sarah@dontcodetired.com", "Hello", false);

            message.Subject = "Hi";

            // act
            sut.Add(message);

            // assert
            Assert.Single(sut.Emails);
        }

        [Fact]
        public void AutoCreation()
        {
            // arrange
            var fixture = new Fixture();
            var sut = new EmailMessageBuffer();

            var message = fixture.Create<EmailMessage>();

            // act
            sut.Add(message);

            // assert
            Assert.Single(sut.Emails);
        }
    }
}