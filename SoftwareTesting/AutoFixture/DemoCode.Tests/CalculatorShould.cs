using AutoFixture.Xunit2;
using Xunit;

namespace DemoCode.Tests
{
    public class CalculatorShould
    {
        [Theory, InlineAutoData, InlineAutoData(-1)]
        [InlineAutoData(0)] // in this case a = 0, b = anonymous positive int
        public void AddTwoPositiveNumbers(int a, int b, Calculator sut)
        {
            sut.Add(a);
            sut.Add(b);

            Assert.Equal(a+b, sut.Value);
        }

        [Theory, AutoData]
        public void AddZeroAndPositiveNumber(int a, int b, Calculator sut)
        {
            sut.Add(a);
            sut.Add(b);

            Assert.Equal(a+b, sut.Value);
        }

        [Fact]
        public void AddNegativeAndPositiveNumber()
        {
            var sut = new Calculator();

            sut.Add(-5);
            sut.Add(2);

            Assert.Equal(-3, sut.Value);
        }
    }
}