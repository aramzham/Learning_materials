using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class CustomizeFixtureDemos
    {
        [Fact]
        public void Error()
        {
            // arrange
            var fixture = new Fixture();

            fixture.Inject("LHR");
            var flight = fixture.Create<FlightDetails>();
            var s = fixture.Create<string>();
            // whenever you ask for a string you will get LHR

            fixture.Inject(new FlightDetails()
            {
                AirlineName = "Lufthansa",
                ArrivalAirportCode = "ZVA",
                DepartureAirportCode = "LHR",
                FlightDuration = TimeSpan.FromHours(4.5)
            });

            // we get exactly same details
            var f1 = fixture.Create<FlightDetails>();
            var f2 = fixture.Create<FlightDetails>();
        }

        [Fact]
        public void CustomCreationFunction()
        {
            // arrange
            var fixture = new Fixture();

            fixture.Register(() => DateTime.Now.Ticks.ToString());

            // you will get ticks as strings
            var s1 = fixture.Create<string>();
            var s2 = fixture.Create<string>();
        }

        [Fact]
        public void FreezingValues()
        {
            var fixture = new Fixture();

            var id = fixture.Freeze<int>(); // ask for an int => get the same value
            var name = fixture.Freeze<string>(); // under the hood uses Inject() method
            var sut = fixture.Create<Order>();

            Assert.Equal(id + "-" + name, sut.ToString());
        }

        [Fact]
        public void OmitSettingSpecificProperties()
        {
            var fixture = new Fixture();

            var flight = fixture.Build<FlightDetails>()
                .Without(x => x.ArrivalAirportCode).Without(x => x.DepartureAirportCode)
                .Create();
        }

        [Fact]
        public void OmitSettingAllProperties()
        {
            var fixture = new Fixture();

            var flight = fixture.Build<FlightDetails>().OmitAutoProperties().Create();
        }

        [Fact]
        public void CustomizeBinding()
        {
            var fixture = new Fixture();

            var flight = fixture.Build<FlightDetails>().With(x => x.ArrivalAirportCode, "ZVA")
                .With(x => x.DepartureAirportCode, "BPA").Create();
        }

        [Fact]
        public void CustomizedBuildingWithActions()
        {
            var fixture = new Fixture();

            var flight = fixture.Build<FlightDetails>()
                .With(x => x.DepartureAirportCode, "LHR")
                .With(x => x.ArrivalAirportCode, "ZVA")
                .Without(x => x.MealOptions)
                .Do(x => x.MealOptions.Add("Chicken"))
                .Do(x => x.MealOptions.Add("Fish"))
                .Create();
        }

        [Fact]
        public void CustomizedBuildingForAllTypesInFixture()
        {
            var fixture = new Fixture();

            fixture.Customize<FlightDetails>(fd =>
                fd.With(x => x.DepartureAirportCode, "LHR")
                    .With(x => x.ArrivalAirportCode, "ZVA")
                    .With(x=>x.AirlineName, "Fly Dubai")
                    .Without(x => x.MealOptions)
                    .Do(x => x.MealOptions.Add("Chicken"))
                    .Do(x => x.MealOptions.Add("Fish"))); // no create here

            var f1 = fixture.Create<FlightDetails>();
            var f2 = fixture.Create<FlightDetails>();
        }
    }
}