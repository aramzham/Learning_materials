using System;
using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class CustomizationDemos
    {
        [Fact]
        public void DateTimeCustomization()
        {
            var fixture = new Fixture();

            //fixture.Customize(new CurrentDateTimeCustomization());
            fixture.Customizations.Add(new CurrentDateTimeGenerator());

            var d1 = fixture.Create<DateTime>();
            var d2 = fixture.Create<DateTime>();
        }

        [Fact]
        public void CustomizedPipeline()
        {
            // arrange
            var fixture = new Fixture();
            fixture.Customizations.Add(new AirportCodeStringPropertyGenerator());

            var flight = fixture.Create<FlightDetails>();
            var airport = fixture.Create<Airport>(); // this also has AirportCode prop => the generator will work
        }
    }
}