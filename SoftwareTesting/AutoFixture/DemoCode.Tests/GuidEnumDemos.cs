using System;
using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class GuidEnumDemos
    {
        [Fact]
        public void GuidAndEnum()
        {
            // arrange
            var fixture = new Fixture();
            var sut = new EmailMessage(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<bool>());

            sut.Id = fixture.Create<Guid>();

            sut.MessageType = fixture.Create<EmailMessageType>(); // returns first value of enum at first run
        }
    }
}