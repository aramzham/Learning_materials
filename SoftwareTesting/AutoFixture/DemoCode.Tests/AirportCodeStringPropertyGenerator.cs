using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace DemoCode.Tests
{
    public class AirportCodeStringPropertyGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            // see if we are trying to create a value for a property
            var propInfo = request as PropertyInfo;

            if (propInfo is null)
            {
                // this specimen builder doesn't apply to current request
                return new NoSpecimen(); // null is a valid specimen so return NoSpecimen
            }

            var isAirportCodeProp = propInfo.Name.Contains("AirportCode");
            var isStringProp = propInfo.PropertyType == typeof(string);

            if (isAirportCodeProp && isStringProp)
                return RandomAirportCode();

            return new NoSpecimen();
        }

        private string RandomAirportCode() => DateTime.Now.Ticks % 2 == 0 ? "LHR" : "PER";
    }
}