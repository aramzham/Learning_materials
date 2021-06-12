using System;
using AutoFixture;
using Xunit;
using Xunit.Sdk;

namespace DemoCode.Tests
{
    public class DateAndTimeDemos
    {
        [Fact]
        public void DateTimes()
        {
            // arrange
            var fixture = new Fixture();
            var logTime = fixture.Create<DateTime>();

            // act
            var result = LogMessageCreator.Create(fixture.Create<string>(), logTime);

            // assert
            Assert.Equal(logTime.Year, result.Year);
        }

        [Fact]
        public void TimeSpans()
        {
            // arrange
            var fixture = new Fixture();

            var timeSpan = fixture.Create<TimeSpan>();
        }
    }
}